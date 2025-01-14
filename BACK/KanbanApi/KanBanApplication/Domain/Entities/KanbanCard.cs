using System.ComponentModel.DataAnnotations;

namespace KanBanApplication.Domain.Entities;

public class KanbanCard
{
    [Key]
    public Guid Id { get; set; }

    public required string Titulo { get; set; }
    
    public required string Conteudo { get; set; }

    public required string Lista { get; set; }
}