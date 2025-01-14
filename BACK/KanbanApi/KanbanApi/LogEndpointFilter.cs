using System.Text.Json;
using KanBanApplication.Domain.Interfaces;
using KanBanApplication.Dtos;

namespace KanbanApi;

public class LogEndpointFilter: IEndpointFilter
{
    private readonly ICrudService<KanbanCardModelDto, KanbanCardDto, Guid> _service;

    public LogEndpointFilter(ICrudService<KanbanCardModelDto, KanbanCardDto, Guid> service)
    {
        _service = service;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var method = context.HttpContext.Request.Method;
        
        if (method.Equals("DELETE",StringComparison.OrdinalIgnoreCase) || (method.Equals("PUT", StringComparison.OrdinalIgnoreCase)))
        {
            var formattedDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            var cardId = context.HttpContext.Request.Path.Value?.Split('/').Last();
            var kanbanCardDto=await _service.Get(Guid.Parse(cardId ?? string.Empty));
            //<datetime> - Card <id> - <titulo> - <Remover|Alterar>
            var operation = method.Equals("DELETE",StringComparison.OrdinalIgnoreCase) ?"Remover":"Alterar";
            Console.WriteLine($"{formattedDateTime} - Card {cardId} - {kanbanCardDto?.Titulo} - {operation}");
        }
        
        return await next(context);
    }
}