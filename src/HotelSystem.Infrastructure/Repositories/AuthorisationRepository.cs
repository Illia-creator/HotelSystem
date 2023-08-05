using HotelSystem.Domain.Entities.ConfigEntities;
using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.OutcomingDtos;
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
    private readonly IPasswordHasher _hasher;
    private readonly TokenRepository _tokenRepository; 

   
    public AuthorisationRepository(
        HotelSystemDbContext context,        
        IPasswordHasher hasher,
        TokenRepository tokenRepository)
    {
        _context = context;
        _hasher = hasher;
        _tokenRepository = tokenRepository;
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

        var token = _tokenRepository.GenerateJwtToken(user);

        var userLoginResponseDto =  new UserLoginResponseDto()
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
            HashPassword = _hasher.Secure(registrationDto.Password),
            Role = "user",
            IsDeleted = false,
            MoneyBonuses = 0
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        var token = _tokenRepository.GenerateJwtToken(user);

        return Task.FromResult(new UserRegistrationResponseDto()
        {
            Success = true,
            Token = token.Result.JwtToken,
            RefreshToken = token.Result.RefreshToken
        });
    }

    Task<VerifyTokenDto> IAuthorisationRepository.VerifyToken(TokenRequestDto requestDto)
    {
        var verifiedStatusOfToken = _tokenRepository.VerifyToken(requestDto);

        if (!verifiedStatusOfToken.ToString().IsNullOrEmpty())
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

        var dbUser = _context.Users.FirstOrDefault(x => x.Id.ToString() == requestDto.Token);

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

        var token = _tokenRepository.GenerateJwtToken(dbUser);

        return Task.FromResult(new VerifyTokenDto()
        { 
            Success = true,
            Token = token.Result.JwtToken,
            RefreshToken = token.Result.RefreshToken
        });
    }
}
