using HotelSystem.Application.HashingUnits;
using HotelSystem.Domain.Entities.ConfigEntities;
using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.OutcomingDtos;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HotelSystem.Infrastructure.Repositories;

public class AuthorisationRepository : IAuthorisationRepository
{
    private readonly HotelSystemDbContext _context;
    private readonly JwtConfig _jwtConfig;
    private readonly TokenValidationParameters _tokenValidationParameters;


    public AuthorisationRepository(
        HotelSystemDbContext context,
        IOptionsMonitor<JwtConfig> optionsMonitor,
        TokenValidationParameters tokenValidationParameters)
    {
        _context = context;
        _jwtConfig = optionsMonitor.CurrentValue;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public Task<UserLoginResponseDto> Login(UserLoginRequestDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == loginDto.Email);

        try
        {
            if (user == null)
                throw new Exception("Password does not found");

            if (!PasswordHasher.Validete(user.HashPassword, loginDto.Password))
                throw new Exception("Wrong Password");
        }
        catch (Exception ex)
        {
            return Task.FromResult(new UserLoginResponseDto()
            {
                Success = false,
                Errors = new List<string>() {
                    ex.Message
                }
            });
        }

        var token = TokenRepository.GenerateJwtToken(user, _jwtConfig, _context);

        var userLoginResponseDto = new UserLoginResponseDto()
        {
            Success = true,
            Token = token.Result.JwtToken,
            RefreshToken = token.Result.RefreshToken
        };

        return Task.FromResult(userLoginResponseDto);

    }

    public Task<UserRegistrationResponseDto> Registration(UserRegistrationRequestDto registrationDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email.Equals(registrationDto.Email));

        if (user != null)
        {
            return Task.FromResult(new UserRegistrationResponseDto()
            {
                Success = false,
                Errors = new List<string>() {
                "Email already in use"
                }
            });
        }

        user = new User()
        {
            Name = registrationDto.Name,
            Surname = registrationDto.Surname,
            PhoneNumber = registrationDto.PhoneNumber,
            Email = registrationDto.Email,
            HashPassword = PasswordHasher.Secure(registrationDto.Password),
            Role = "user",
            IsDeleted = false,
            MoneyBonuses = 0
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        var token = TokenRepository.GenerateJwtToken(user, _jwtConfig, _context);

        return Task.FromResult(new UserRegistrationResponseDto()
        {
            Success = true,
            Token = token.Result.JwtToken,
            RefreshToken = token.Result.RefreshToken
        });
    }

    Task<VerifyTokenDto> IAuthorisationRepository.VerifyToken(TokenRequestDto requestDto)
    {
        var verifiedStatusOfToken = TokenRepository.VerifyToken(requestDto, _tokenValidationParameters, _context);

        if (!verifiedStatusOfToken.Result.ToString().IsNullOrEmpty())
        {
            return Task.FromResult(new VerifyTokenDto()
            {
                Success = false,
                Errors = new List<string>()
                {
                    verifiedStatusOfToken.Result
                }
            });
        }

        var dbUser = _context.RefreshTokens
            .Where(t => t.Token == requestDto.RefreshToken)
            .Join(_context.Users,
            t => t.UserId,
            u => u.Id,
            (t, u) => u)
            .FirstOrDefault();
                     

        if (dbUser == null)
        {
            return Task.FromResult(new VerifyTokenDto()
            {
                Success = false,
                Errors = new List<string>()
                {
                    "User By Token Not Found"
                }
            });
        }

        var token = TokenRepository.GenerateJwtToken(dbUser, _jwtConfig, _context);

        return Task.FromResult(new VerifyTokenDto()
        {
            Success = true,
            Token = token.Result.JwtToken,
            RefreshToken = token.Result.RefreshToken
        });
    }
}
