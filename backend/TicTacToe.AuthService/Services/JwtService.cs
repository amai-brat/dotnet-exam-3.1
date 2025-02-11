using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.AuthService.Abstractions.Services;
using TicTacToe.AuthService.Entities;
using TicTacToe.AuthService.Options;

namespace TicTacToe.AuthService.Services;

public class JwtService(IOptionsMonitor<JwtOptions> jwtOptionsMonitor): IJwtService
{
    private readonly JwtOptions _jwtOptions = jwtOptionsMonitor.CurrentValue;
    
    public string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Login)
        };
            
        var jwt = CreateJwtToken(claims);
        
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            
        return token;
    }
    
    private JwtSecurityToken CreateJwtToken(List<Claim> claims)
    {
        var now = DateTime.UtcNow;
        return new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            notBefore: now,
            claims: claims,
            expires: now.Add(TimeSpan.FromHours(2)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
                SecurityAlgorithms.HmacSha256)
        );
    }

}