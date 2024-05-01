using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserServiceOina.model;

namespace UserServiceOina.service.impl;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateToken(JwtUserDetails jwtUserDetails)
    {
        var claims = CreateClaims(jwtUserDetails);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["jwt:tokenLifespanInMinutes"])),
            SigningCredentials = creds,
            Issuer = null,
            Audience = null
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    private IList<Claim> CreateClaims(JwtUserDetails jwtUserDetails)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, jwtUserDetails.RenterId.ToString()),
            new(JwtRegisteredClaimNames.Email, jwtUserDetails.Login),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        foreach (var role in jwtUserDetails.RolesList)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}