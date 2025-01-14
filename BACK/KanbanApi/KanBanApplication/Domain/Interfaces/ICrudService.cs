using OneOf;
using OneOf.Types;

namespace KanBanApplication.Domain.Interfaces;

public interface ICrudService<TDto, TKey>
{
    Task<TDto> Create(TDto model);

    Task<OneOf<NotFound, TDto>> Update(TKey id,TDto model);

    Task<OneOf<NotFound,Success>> Delete(TKey idEntity);

    Task<List<TDto>> GetAll();
}