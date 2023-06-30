using HotelSystem.Domain.Entities.Dtos;
using HotelSystem.Domain.Entities.Dtos.ReservationDtos;

namespace HotelSystem.Domain.Repositories;
public interface IReservationRepository
{
    Task CreateReservation(CreateReservationDto reservationDto);
    Task CancelResrtvation(Guid id);
}