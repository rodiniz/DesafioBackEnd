using System.ComponentModel.DataAnnotations;

namespace KanBanApplication.Dtos;

public class KanbanCardModelDto
{
    [Required]
    public required string Titulo { get; set; }
    [Required]
    public required string Conteudo { get; set; }
    [Required]
    public required string Lista { get; set; }
}