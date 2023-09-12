using BuberDinner.Application.Services;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace BuberDinner.Api.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest register)
    {
        var authResult = _authenticationService.Register(register.FirstName, register.LastName, register.Email, register.Password);
        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName, 
            authResult.Email,
            authResult.Token
            );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest login)
    {
        var authResult = _authenticationService.Login(login.Email, login.Password);
        var response = new AuthenticationResponse(
        authResult.Id,
        authResult.FirstName,
        authResult.LastName,
        authResult.Email,
        authResult.Token
        );
        return Ok(response);
    }
}