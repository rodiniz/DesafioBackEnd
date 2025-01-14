namespace KanBanApplication.Dtos;

public class KanbanCardInsertDto
{
    public required string Titulo { get; set; }
    
    public required string Conteudo { get; set; }

    public required string Lista { get; set; }
}