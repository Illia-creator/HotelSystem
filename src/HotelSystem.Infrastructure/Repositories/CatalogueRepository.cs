using HotelSystem.Domain.Entities;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.HotelRequests;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.RoomRequests;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Persistence.DbContexts;
using Npgsql.Internal.TypeHandlers;
using HotelSystem.Application.JsonHandlers;
using JsonHandler = HotelSystem.Application.JsonHandlers.JsonHandler;
using HotelSystem.Application.FiltersHandler;

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

         var result = FilterHandler.GetFilterRequest(_context.Hotels, noDefaultValuesJson) ;

         return Task.FromResult(result);
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