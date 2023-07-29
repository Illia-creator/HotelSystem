using HotelSystem.Domain.Entities.ConfigEntities;
using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelSystem.Infrastructure.Repositories;

public class AuthorisationRepository : IAuthorisationRepository
{
    private readonly HotelSystemDbContext _context;
    private readonly JwtConfig _jwtConfig;

    public AuthorisationRepository(HotelSystemDbContext context, IOptionsMonitor<JwtConfig> optionsMonitor)
    {
        _context = context;
        _jwtConfig = optionsMonitor.CurrentValue;
    }

    public Task<UserLoginResponseDto> Login(UserLoginRequestDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == loginDto.Email);

        try
        {
            if (user == null)
                throw new Exception("Password does not found");            

            if (user.HashPassword != loginDto.Password)
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
            Token = token
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
            HashPassword = registrationDto.Password,
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
            Token = token
        });
    }

    private string GenerateJwtToken(User user)
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
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
        };

        var token = jwtHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtHandler.WriteToken(token);

        return jwtToken;
    }
}
