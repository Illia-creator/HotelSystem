using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.HotelRequests;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.RoomRequests;
using System.Linq.Expressions;

namespace HotelSystem.Domain.Repositories;
public interface ICatalogueRepository
{
    Task<IQueryable<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(Guid id);
    Task<IQueryable<Hotel>> GetHotelsByFilters(HotelFilterRequest filterRequest);

    Task<IQueryable<Room>> GetRoomsByFilters(RoomFilterRequest filterRequest);
    Task<IQueryable<Room>> GetAllRoomsByHotel(Guid hotelId);  
    Task<Room> GetRoomById(Guid id);    
}
