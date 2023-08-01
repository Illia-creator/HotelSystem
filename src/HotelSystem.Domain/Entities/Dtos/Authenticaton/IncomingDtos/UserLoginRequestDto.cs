namespace HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;

public class UserLoginRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
