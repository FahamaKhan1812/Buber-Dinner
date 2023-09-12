using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Services;
using Microsoft.IdentityModel.Tokens;

namespace BuberDinner.Infrastructure;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Guid userId, string FirstName, string LastName)
    {
        var sigingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")),
            SecurityAlgorithms.HmacSha256
            );

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var securityToken = new JwtSecurityToken(
            issuer: FirstName+"-"+LastName,
            expires: _dateTimeProvider.UtcNow.AddMinutes(60),
            claims: claims, signingCredentials: sigingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
