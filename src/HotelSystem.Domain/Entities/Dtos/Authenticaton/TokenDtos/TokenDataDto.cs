namespace HotelSystem.Domain.Entities.Dtos.Authenticaton.TokenDtos;

public class TokenDataDto
{
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}
