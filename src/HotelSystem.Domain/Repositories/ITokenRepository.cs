using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.TokenDtos;

namespace HotelSystem.Domain.Repositories;

public interface ITokenRepository
{
    public Task<TokenDataDto> GenerateJwtToken(User user);
    public Task<string> VerifyToken(TokenRequestDto tokenDto);
}
