using KanBanApplication.Domain.Entities;
using KanBanApplication.Dtos;
using Riok.Mapperly.Abstractions;

namespace KanBanApplication;

[Mapper]
public static partial class KanbanMapper
{
    public static partial KanbanCardDto EntityToDto(KanbanCard car);
    public static partial KanbanCard DtoToEntity(KanbanCardDto car);
    
    public static partial IQueryable<KanbanCardDto> EntityToDto(this IQueryable<KanbanCard> q);
}