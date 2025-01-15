using KanBanApplication.Dtos;
using OneOf;
using OneOf.Types;

namespace KanBanApplication.Domain.Interfaces;

public interface IKanbanCrudService
{
    Task<KanbanCardDto> Create(KanbanCardModelDto model);

    Task<OneOf<NotFound, KanbanCardModelDto>> Update(Guid id,KanbanCardModelDto model);

    Task<OneOf<NotFound,List<KanbanCardDto>>> Delete(Guid idEntity);

    Task<List<KanbanCardDto>> GetAll();
    
    Task<KanbanCardDto?> Get(Guid id);
}