using FluentValidation;
using KanBanApplication.Dtos;

namespace KanBanApplication.InfraStructure.Validators;

public class KanbanCardValidator: AbstractValidator<KanbanCardModelDto> 
{
    public KanbanCardValidator()
    {
        RuleFor(card => card.Titulo).NotNull().NotEmpty();
        RuleFor(card => card.Conteudo).NotNull().NotEmpty();
        RuleFor(card => card.Lista).NotNull().NotEmpty();
    }
}