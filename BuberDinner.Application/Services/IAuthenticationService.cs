﻿namespace BuberDinner.Application.Services;
public interface IAuthenticationService
{
    AuthenticationResult Register(string FirstName,
        string LastName,
        string Email,
        string Password
);
    AuthenticationResult Login(
        string Email,
        string Password
);
}