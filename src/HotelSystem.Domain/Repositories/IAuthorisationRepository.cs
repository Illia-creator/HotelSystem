using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.OutcomingDtos;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.TokenDtos;

namespace HotelSystem.Domain.Repositories;

public interface IAuthorisationRepository
{
    Task<UserRegistrationResponseDto> Registration(UserRegistrationRequestDto registrationDto);
    Task<UserLoginResponseDto> Login(UserLoginRequestDto loginDto);
    Task<VerifyTokenDto> VerifyToken(TokenRequestDto requestDto);
}
