using System.Reflection.Metadata;

namespace HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;

public class AuthResult
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
}
