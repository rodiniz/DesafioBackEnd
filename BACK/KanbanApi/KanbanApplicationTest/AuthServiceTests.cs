using System.IdentityModel.Tokens.Jwt;
using AutoFixture;
using KanBanApplication;
using KanBanApplication.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace KanbanApplicationTest;

public class AuthServiceTests
{
    private readonly Fixture _fixture = new();
   
    [Fact]
    public void login_with_valid_credentials_returns_jwt_token()
    {
        // Arrange
        var userName = _fixture.Create<string>();
        var password = _fixture.Create<string>();
        var jwtTokenSecret = _fixture.Create<string>();
        var loginSettings = new LoginSettings { Username =userName, Password = password };
        var jwtSettings = new JwtSettings { JWTSecret = jwtTokenSecret };

        var loginOptionsMonitor = Mock.Of<IOptions<LoginSettings>>(x => x.Value == loginSettings);
        var jwtOptionsMonitor = Mock.Of<IOptions<JwtSettings>>(x => x.Value == jwtSettings);

        var authService = new AuthService(loginOptionsMonitor, jwtOptionsMonitor);

        // Act
        var token = authService.Login(userName, password);

        // Assert
        Assert.False(string.IsNullOrEmpty(token));
        var handler = new JwtSecurityTokenHandler();
        Assert.True(handler.CanReadToken(token));
    }


    [Fact]
    public void login_with_invalid_credentials_returns_empty_string()
    {
        // Arrange
        var userName = _fixture.Create<string>();
        var password = _fixture.Create<string>();
        var jwtTokenSecret = _fixture.Create<string>();
        var loginSettings = new LoginSettings { Username = userName, Password = password };
        var jwtSettings = new JwtSettings { JWTSecret = jwtTokenSecret };

        var loginOptionsMonitor = Mock.Of<IOptions<LoginSettings>>(x => x.Value == loginSettings);
        var jwtOptionsMonitor = Mock.Of<IOptions<JwtSettings>>(x => x.Value == jwtSettings);

        var authService = new AuthService(loginOptionsMonitor, jwtOptionsMonitor);

        // Act
        var token = authService.Login(_fixture.Create<string>(), _fixture.Create<string>());

        // Assert
        Assert.Empty(token);
    }

   
    [Fact]
    public void handle_empty_username_or_password()
    {
        // Arrange
        var userName = _fixture.Create<string>();
        var password = _fixture.Create<string>();
        var jwtTokenSecret = _fixture.Create<string>();
        var loginSettings = new LoginSettings { Username = userName, Password =password };
        var jwtSettings = new JwtSettings { JWTSecret = jwtTokenSecret };

        var loginOptionsMonitor = Mock.Of<IOptions<LoginSettings>>(x => x.Value == loginSettings);
        var jwtOptionsMonitor = Mock.Of<IOptions<JwtSettings>>(x => x.Value == jwtSettings);

        var authService = new AuthService(loginOptionsMonitor, jwtOptionsMonitor);

        // Act
        var emptyUsernameResult = authService.Login("", password);
        var emptyPasswordResult = authService.Login(userName,  "");
        var bothEmptyResult = authService.Login("", "");

        // Assert
        Assert.Equal(string.Empty, emptyUsernameResult);
        Assert.Equal(string.Empty, emptyPasswordResult);
        Assert.Equal(string.Empty, bothEmptyResult);
    }
}