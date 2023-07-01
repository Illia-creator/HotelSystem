using HotelSystem.Domain.Entities;
using HotelSystem.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogueController : ControllerBase
{
    private readonly ICatalogueRepository _repository;

    public CatalogueController(ICatalogueRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("hotels")]
    public async Task<IActionResult> GetAllHotels()
    {
        var result = await _repository.GetAllHotels();
        return Ok(result);
    }

    [HttpGet]
    [Route("hotel_rooms")]
    public async Task<IActionResult> GetRoomsByHotelId(Guid hotelId)
    {
        var result = await _repository.GetAllRoomsByHotel(hotelId);
        return Ok(result);
    }

    [HttpGet]
    [Route("hotel_id")]
    public async Task<IActionResult> GetHotelById(Guid hotelId)
    { 
        var result = _repository.GetHotelById(hotelId);
        return Ok(result);
    }


}
