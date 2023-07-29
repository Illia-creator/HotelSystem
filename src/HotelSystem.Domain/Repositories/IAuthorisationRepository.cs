using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;

namespace HotelSystem.Domain.Repositories;

public interface IAuthorisationRepository
{
    Task<UserRegistrationResponseDto> Registration(UserRegistrationRequestDto registrationDto);
    Task<UserLoginResponseDto> Login( UserLoginRequestDto loginDto);
}
