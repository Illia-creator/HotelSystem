using HotelSystem.Domain.Entities.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;

public class TokenRequestDto
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }
}
