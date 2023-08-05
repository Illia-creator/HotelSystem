using HotelSystem.Domain.Entities.ConfigEntities;
using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.TokenDtos;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelSystem.Infrastructure.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly JwtConfig _jwtConfig;
    private readonly HotelSystemDbContext _context;
    private readonly TokenValidationParameters _tokenValidationParameters;


    public TokenRepository(IOptionsMonitor<JwtConfig> optionsMonitor,
        HotelSystemDbContext context, 
        TokenValidationParameters tokenValidationParameters)
    {
        _jwtConfig = optionsMonitor.CurrentValue;
        _context = context;
        _tokenValidationParameters = tokenValidationParameters;
    }

    public async Task<TokenDataDto> GenerateJwtToken(User user)
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

    public Task<string> VerifyToken(TokenRequestDto tokenDto)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(tokenDto.Token, _tokenValidationParameters, out var validatedToken);

        string errorMessage = string.Empty;

        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        {
            var jwtAlg = jwtSecurityToken
                .Header
                .Alg
                .Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);

            if (!jwtAlg)
                return null;

            var utcExpiryDate =
                long.Parse(principal
                    .Claims
                    .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)
                    .Value);

            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

            if (expiryDate > DateTime.UtcNow)
                errorMessage = "Jwt Token Has Not Expired";
        }

        var refreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == tokenDto.RefreshToken);

        if (refreshToken is null)
            errorMessage = "Invalid Refresh Token";

        if (refreshToken.ExpiryDate < DateTime.UtcNow)
            errorMessage = "Refresh Token Has Expired. Login Again";

        if (refreshToken.IsUsed)
            errorMessage = "Refresh Token Has Been Used And Cannot Be Reused";

        if (refreshToken.IsRevoked)
            errorMessage = "Refresh Token Has Been Revoked And Cannot Be Used";

        var jti = principal.Claims.SingleOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        if (refreshToken.JwtId != jti)
            errorMessage = "Refresh Token Reference Does Not Match The Jwt Token";

            refreshToken.IsUsed = true;

        _context.RefreshTokens.Update(refreshToken);

        _context.SaveChanges();

        return Task.FromResult(errorMessage);
    }

    private string RandomStringGenerator(int length)
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(
            Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
    }

    private DateTime UnixTimeStampToDateTime(long unixDate)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, 0, DateTimeKind.Utc);

        dateTime.AddSeconds(unixDate).ToUniversalTime();

        return dateTime;
    }
}
