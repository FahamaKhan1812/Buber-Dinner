using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepositry _userRepositry;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepositry userRepositry)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepositry = userRepositry;
    }

    public AuthenticationResult Login(string Email, string Password)
    {
        // Validate user
        if(_userRepositry.GetUserByEmail(Email) is not User user)
        {
            throw new Exception("User is not exists with this email "+ Email);
        }
        // Validate the password
        if(user.Password != Password)
        {
            throw new Exception("Invalid password");
        }
        // Create the JWT
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

        return new AuthenticationResult(user.Id, user.FirstName,user.LastName, user.Email, user.Password, token);
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
        // Check if the user already exits
        if (_userRepositry.GetUserByEmail(Email) is not null)
        {
            throw new Exception("User is already register with this email " + Email);
        }
        // Create user (generate unique Id)
        var user = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };
        _userRepositry.Add(user);
        // Create JWT 
        var token = _jwtTokenGenerator.GenerateToken(user.Id, FirstName, LastName);

        return new AuthenticationResult(user.Id, FirstName, LastName, Email, Password, token);
    }
}

