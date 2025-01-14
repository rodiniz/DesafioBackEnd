using FluentValidation;
using KanBanApplication.Domain.Interfaces;
using KanBanApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KanbanApi.Routes;

public static class KanbanRoutes
{
    public static void AddCourseRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/cards").RequireAuthorization()
            .AddEndpointFilter<LogEndpointFilter>();
        
        group.MapPost("/", async (
                [FromServices] ICrudService<KanbanCardModelDto, KanbanCardDto, Guid> service,
                [FromBody] KanbanCardModelDto kanbanCardModelDto,
                IValidator<KanbanCardModelDto> validator) =>
            {
                var validationResult = await validator.ValidateAsync(kanbanCardModelDto);

                if (!validationResult.IsValid) 
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                var createResult = await service.Create(kanbanCardModelDto);
                return Results.Created("", createResult);
            })
            .WithOpenApi();

        group.MapGet("/",
            ([FromServices] ICrudService<KanbanCardModelDto, KanbanCardDto, Guid> courseService) =>
                courseService.GetAll());
        group.MapPut("/{id}", async (
            [FromServices] ICrudService<KanbanCardModelDto, KanbanCardDto, Guid> courseService, 
            Guid id,
            [FromBody] KanbanCardModelDto kanbanCardModelDto,
            IValidator<KanbanCardModelDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(kanbanCardModelDto);

            if (!validationResult.IsValid) 
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }
            var result = await courseService.Update(id, kanbanCardModelDto);
            return result.Match(
                notfound => Results.NotFound(),
                okResult => Results.Json(okResult)
            );
        });
        group.MapDelete("/{id}",
            async ([FromServices] ICrudService<KanbanCardModelDto, KanbanCardDto, Guid> courseService,
                Guid id) =>
            {
                var deleteResult = await courseService.Delete(id);
                return deleteResult.Match(
                    notfound => Results.NotFound(),
                    Results.Ok
                );
            });
    }
}