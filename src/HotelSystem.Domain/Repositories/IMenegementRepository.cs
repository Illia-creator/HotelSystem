using HotelSystem.Domain.Entities;
using HotelSystem.Domain.Entities.Dtos;
using HotelSystem.Domain.Entities.Dtos.HotelDtos;
using HotelSystem.Domain.Entities.Dtos.PaymentDtos;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.PaymentsRequests;
using HotelSystem.Domain.Entities.Dtos.RoomDtos;
using HotelSystem.Domain.Entities.Dtos.RoomTypeDtos;

namespace HotelSystem.Domain.Repositories;
public interface IMenegementRepository 
{
    #region Hotels Region
    Task CreateHotel(CreateHotelDto hotelDto);
    Task UpdateHotel(UpdateHotelDto hotelDto);
    Task DeleteHotel(Guid hotelId);
    #endregion

    #region Rooms Region
    Task<IEnumerable<Room>> GetAllRooms();
    Task CreateRoom(CreateRoomDto roomDto);
    Task UpdateRoom(UpdateRoomDto roomDto);
    Task DeleteRoom(Guid id);
    #endregion

    #region Payments Region
    Task<IEnumerable<Payment>> GetAllPayments();
    Task<IEnumerable<Payment>> GetPaymentByUserName(string name);
    Task<IEnumerable<Payment>> GetPaymentsByFilter(PaymentFilterRequest paymentFilter);
    Task UpdatePayment(UpdatePaymentDto paymentDto);
    #endregion  

    #region Room Types Region
    Task<IEnumerable<RoomType>> GetAllRoomTypes();
    Task CreateRoomType(CreateRoomTypeDto roomDto);
    Task UpdateRoomType(UpdateRoomTypeDto roomDto);
    Task DeleteRoomType(Guid id);
    Task<Room> GetRoomTypeById(Guid id);
    #endregion

    #region Reservation Region
    Task UpdateReservation(UpdateReservationDto reservationDto);
    Task DeleteReservation(Guid id);
    Task GetReservationByUserName(string name);
    Task<IEnumerable<Reservation>> GetReservationByFilter();
    #endregion

         

}