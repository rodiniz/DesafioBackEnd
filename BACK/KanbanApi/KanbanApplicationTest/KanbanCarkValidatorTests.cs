using AutoFixture;
using KanBanApplication.Dtos;
using KanBanApplication.InfraStructure.Validators;

public class KanbanCardValidatorTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void validate_card_with_all_required_fields_should_pass()
    {
        var validator = new KanbanCardValidator();

        var card = _fixture.Create<KanbanCardModelDto>();

        var result = validator.Validate(card);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void validate_card_without_titulo_should_not_pass()
    {
        var validator = new KanbanCardValidator();

        var card = _fixture.Build<KanbanCardModelDto>()
            .Without(x => x.Titulo)
            .Create();

        var result = validator.Validate(card);

        Assert.False(result.IsValid);
    }
    
    [Fact]
    public void validate_card_without_conteudo_should_not_pass()
    {
        var validator = new KanbanCardValidator();

        var card = _fixture.Build<KanbanCardModelDto>()
            .Without(x => x.Conteudo)
            .Create();

        var result = validator.Validate(card);

        Assert.False(result.IsValid);
    }
    [Fact]
    public void validate_card_without_lista_should_not_pass()
    {
        var validator = new KanbanCardValidator();

        var card = _fixture.Build<KanbanCardModelDto>()
            .Without(x => x.Lista)
            .Create();

        var result = validator.Validate(card);

        Assert.False(result.IsValid);
    }
}