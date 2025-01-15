using KanBanApplication.Domain.Interfaces;
using KanBanApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KanbanApi.Routes;

public static class AuthRoutes
{
    public static void AddAuthRoutes(this WebApplication app)
    {
        app.MapPost("/login",  (
                [FromBody] LoginRequestDto loginRequestDto,
                [FromServices] IAuthService authService) =>
            {
                var result =  authService.Login(loginRequestDto.Login,loginRequestDto.Senha);
                
                return !string.IsNullOrEmpty(result)?  Results.Json(result): Results.BadRequest();
            })
            .WithOpenApi();
    }
        
}