using HotelSystem.Domain.Entities;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.HotelRequests;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.RoomRequests;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.Repositories;
public class CatalogueRepository : ICatalogueRepository
{
    private readonly HotelSystemDbContext _context;

    public CatalogueRepository(HotelSystemDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Hotel>> GetAllHotels()
    {
        var hotels = _context.Hotels;
        return hotels;
    }

    public async Task<IQueryable<Room>> GetAllRoomsByHotel(Guid hotelId)
    {
        var rooms = _context.Rooms.Where(r => r.HotelId == hotelId);
         
        if (rooms.FirstOrDefault() == null)
            throw new Exception("No rooms in hotel found!");

        return rooms;
    }

    public Task<Hotel> GetHotelById(Guid id)
    {
        var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);

        if (hotel == null)
            throw new Exception("No hotel found!");

        return Task.FromResult(hotel);
    }

    public Task<IQueryable<Hotel>> GetHotelsByFilters(HotelFilterRequest filterRequest)
    {
        throw new NotImplementedException();
    }

    public Task<Room> GetRoomById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Room>> GetRoomsByFilters(RoomFilterRequest filterRequest)
    {
        throw new NotImplementedException();
    }
}