using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.ReservationDtos;

namespace HotelSystem.Domain.Repositories;
public interface IBookingRepository
{
    Task<Reservation> CreateResvation(CreateReservationDto reservationDto);
    Task CancelResrtvation(Guid id);
}