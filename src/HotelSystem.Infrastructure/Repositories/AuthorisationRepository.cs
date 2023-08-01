using HotelSystem.Domain.Entities.ConfigEntities;
using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.TokenDtos;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Hashing;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace HotelSystem.Infrastructure.Repositories;

public class AuthorisationRepository : IAuthorisationRepository
{
    private readonly HotelSystemDbContext _context;
    private readonly JwtConfig _jwtConfig;
    private readonly IPasswordHasher _hasher;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public AuthorisationRepository(
        HotelSystemDbContext context,
        IOptionsMonitor<JwtConfig> optionsMonitor,
        IPasswordHasher hasher, 
        TokenValidationParameters tokenValidationParameters)
    {
        _context = context;
        _jwtConfig = optionsMonitor.CurrentValue;
        _hasher = hasher;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public Task<UserLoginResponseDto> Login(UserLoginRequestDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == loginDto.Email);

        try
        {
            if (user == null)
                throw new Exception("Password does not found");            

            if (!_hasher.Validete(user.HashPassword, loginDto.Password))
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

        var token = GenerateJwtToken(user);

        return Task.FromResult(new UserLoginResponseDto()
        {
            Success = true,
            Token = token.JwtToken,
            RefreshToken = token.RefreshToken
        });
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
            HashPassword = _hasher.Secure(registrationDto.Password),
            Role = "user",
            IsDeleted = false,
            MoneyBonuses = 0
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        var token = GenerateJwtToken(user);

        return Task.FromResult(new UserRegistrationResponseDto()
        {
            Success = true,
            Token = token.JwtToken,
            RefreshToken = token.RefreshToken
        });
    }

    private TokenDataDto GenerateJwtToken(User user)
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        IdentityUser tokenUser = new IdentityUser()
        {
            Id = user.Id.ToString(),
            Email = user.Email
        };

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", tokenUser.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.Add(_jwtConfig.ExpiryTimeFrame),
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtHandler.WriteToken(token);

        var refreshToken = new RefreshToken()
        { 
            Token = $"{RandomStringGenerator(25)}_{Guid.NewGuid()}",
            UserId = user.Id.ToString(),
            IsRevoked = false,
            IsUsed = false,
            JwtId = token.Id,
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };

        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();

        var tokenData = new TokenDataDto
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken.Token
        };

        return tokenData;
    }

    private string RandomStringGenerator(int length)
    { 
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}
