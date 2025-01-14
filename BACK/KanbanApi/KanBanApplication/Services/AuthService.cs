using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KanBanApplication.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace KanBanApplication.Services;

public class AuthService : IAuthService
{
    private readonly LoginSettings _loginSettings;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IOptions<LoginSettings> loginSettings, IOptions<JwtSettings> jwtSettings)
    {
        _loginSettings = loginSettings.Value;
        _jwtSettings = jwtSettings.Value;
    }

    public string Login(string login, string senha)
    {
        if (login == _loginSettings.Username && senha == _loginSettings.Password)
        {
            return GenerateJwtToken(login);
        }

        return string.Empty;
    }

    private string GenerateJwtToken(string login)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, login)
        };
        var secret = _jwtSettings.JWTSecret;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}