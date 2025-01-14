using KanBanApplication.Dtos;
using OneOf;
using OneOf.Types;

namespace KanBanApplication.Domain.Interfaces;

public interface ICrudService<TCreateDto, TUpdateDto, TKey>
{
    Task<TUpdateDto> Create(TCreateDto model);

    Task<OneOf<NotFound, TCreateDto>> Update(TKey id,TCreateDto model);

    Task<OneOf<NotFound,Success>> Delete(TKey idEntity);

    Task<List<TUpdateDto>> GetAll();
    
    Task<KanbanCardDto?> Get(TKey id);
}