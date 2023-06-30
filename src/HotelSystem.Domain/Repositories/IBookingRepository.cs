using HotelSystem.Domain.Entities;
using HotelSystem.Domain.Entities.Dtos.ReservationDtos;

namespace HotelSystem.Domain.Repositories;
public interface IBookingRepository
{
    Task<Reservation> CreateResvation(CreateReservationDto reservationDto);
}