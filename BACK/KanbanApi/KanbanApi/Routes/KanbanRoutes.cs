using KanBanApplication.Domain.Interfaces;
using KanBanApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace KanbanApi.Routes;

public static class KanbanRoutes
{
    public static void AddCourseRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/cards").RequireAuthorization();
        group.MapPost("/", async (
                [FromServices] ICrudService<KanbanCardDto,Guid> service,
                [FromBody] KanbanCardDto courseDto) =>
            {
                var createResult= await service.Create(courseDto);
                return  Results.Created("",createResult);
            })
            .WithOpenApi();

        group.MapGet("/", ([FromServices] ICrudService<KanbanCardDto,Guid> courseService) => courseService.GetAll());
        group.MapPut("/{id}", async ([FromServices] ICrudService<KanbanCardDto,Guid> courseService, Guid id, [FromBody] KanbanCardDto courseDto) =>
        {
            var result=await courseService.Update(id, courseDto);
            return result.Match(
                    notfound=> Results.NotFound(),
                    okResult => Results.Ok(okResult)
                );
        });
        group.MapDelete("/{id}", async ([FromServices] ICrudService<KanbanCardDto,Guid> courseService, Guid id) =>
        {

           var deleteResult= await courseService.Delete(id);
           return deleteResult.Match(
               notfound=> Results.NotFound(),
               Results.Ok
           );
        });
    }
}