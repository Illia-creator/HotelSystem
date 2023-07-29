 using HotelSystem.Application.JsonHandlers;
using HotelSystem.Domain.Entities;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.HotelRequests;
using HotelSystem.Domain.Entities.Dtos.RequestDtos.RoomRequests;
using HotelSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelSystem.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        var result = await _repository.GetHotelById(hotelId);
        return Ok(result);
    }

    [HttpGet]
    [Route("filter_hotels")]
    public async Task<IActionResult> GetHotelsByFilters([FromQuery]HotelFilterRequest filter)
    {
        var result = await _repository.GetHotelsByFilters(filter);
        return Ok(result);    
    }

    [HttpGet]
    [Route("room_id")]
    public async Task<IActionResult> GetRoomById(Guid id)
    { 
        var result = await _repository.GetRoomById(id);
        return Ok(result);
    }

    [HttpGet]
    [Route("filter_rooms")]
    public async Task<IActionResult> GetRoomsByFilter([FromQuery]RoomFilterRequest filter)
    {
        var result = await _repository.GetRoomsByFilters(filter);
        return Ok(result);
    }
}
