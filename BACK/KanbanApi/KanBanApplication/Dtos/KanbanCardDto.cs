namespace KanBanApplication.Dtos;

public class KanbanCardDto: KanbanCardInsertDto
{
    public required Guid Id { get; set; }
}