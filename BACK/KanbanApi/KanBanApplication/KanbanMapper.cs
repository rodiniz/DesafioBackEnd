using KanBanApplication.Domain.Entities;
using KanBanApplication.Dtos;
using Riok.Mapperly.Abstractions;

namespace KanBanApplication;

[Mapper]
public static partial class KanbanMapper
{
    public static partial KanbanCardDto EntityToDto(KanbanCard kanbanCard);
    public static partial KanbanCard DtoToEntity(KanbanCardDto kanbanCard);

    public static KanbanCard DtoToEntity(KanbanCardModelDto kanbanCard)
    {
        return new KanbanCard{Conteudo = kanbanCard.Conteudo, Lista = kanbanCard.Lista, Titulo = kanbanCard.Titulo};
    }
    
    public static partial IQueryable<KanbanCardDto> EntityToDto(this IQueryable<KanbanCard> kanbanCards);
}