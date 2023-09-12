namespace BuberDinner.Application.Services;
public record AuthenticationResult(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Token
    );
