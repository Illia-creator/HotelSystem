using HotelSystem.Domain.Entities.ConfigEntities;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Incoming;
using HotelSystem.Domain.Entities.Dtos.Authenticaton.Outcoming;
using HotelSystem.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace HotelSystem.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthorisationRepository _repository;
    public AuthenticationController(IAuthorisationRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    [Route("registration")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto registrationDto)
    {
        var result = _repository.Registration(registrationDto);

        return Ok(result);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto LoginDto)
    {
        var result = _repository.Login(LoginDto);

        return Ok(result);
    }
}
