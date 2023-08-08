using HotelSystem.Application.FiltersHandler;
using HotelSystem.Domain.Entities.DbEntities;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.HotelRequests;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.RoomRequests;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Microsoft.AspNetCore.Http;
using JsonHandler = HotelSystem.Application.JsonHandlers.JsonHandler;

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

    public async Task<IQueryable<Hotel>> GetAllHotelsWithUser(HttpContext httpContext)
    {
        var UserId = httpContext.User.Claims.FirstOrDefault().Value;

        var hotels = _context.Hotels;
        return hotels;
    }


    public async Task<IQueryable<Room>> GetAllRoomsByHotel(Guid hotelId)
    {
        var rooms = _context.Rooms.Where(r => r.HotelId == hotelId);

        if (rooms.FirstOrDefault() == null)
            throw new Exception($"No rooms in hotel with id: {hotelId} found!");

        return rooms;
    }

    public Task<Hotel> GetHotelById(Guid id)
    {
        var hotel = _context.Hotels.FirstOrDefault(h => h.Id == id);

        if (hotel == null)
            throw new Exception($"No hotel with id: {id} found!");

        return Task.FromResult(hotel);
    }

    public Task<IQueryable<Hotel>> GetHotelsByFilters(HotelFilterRequest filterRequest)
    {
        var noDefaultValuesJson = JsonHandler.ClearDefaultValues(filterRequest);

        var hotels = FilterHandler.GetFilterRequest(_context.Hotels, noDefaultValuesJson);

        if (hotels == null)
            throw new Exception($"No hotels with such parameters were found!");

        return Task.FromResult(hotels);
    }

    public Task<Room> GetRoomById(Guid id)
    {
        var room = _context.Rooms.FirstOrDefault(r => r.Id == id);

        if (room == null)
            throw new Exception($"No rooms in hotel with id: {id} found!");

        return Task.FromResult(room);
    }

    public Task<IQueryable<Room>> GetRoomsByFilters(RoomFilterRequest filterRequest)
    {
        var noDefaultValuesJson = JsonHandler.ClearDefaultValues(filterRequest);

        var rooms = FilterHandler.GetFilterRequest(_context.Rooms, noDefaultValuesJson);

        if (rooms == null)
            throw new Exception($"No rooms with such parameters were found!");

        return Task.FromResult(rooms);
    }
}