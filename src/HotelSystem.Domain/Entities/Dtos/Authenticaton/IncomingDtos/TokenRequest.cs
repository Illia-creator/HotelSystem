using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;

public class TokenRequest
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}
