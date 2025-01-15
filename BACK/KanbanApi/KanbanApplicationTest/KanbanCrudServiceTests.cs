using AutoFixture;
using KanBanApplication.Dtos;
using KanBanApplication.InfraStructure.Persistence;
using KanBanApplication.Services;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace KanbanApplicationTest;

public class KanbanCrudServiceTests
{
    private readonly Fixture _fixture = new();
    private readonly KanbanContext _kanbanContext;
    public KanbanCrudServiceTests()
    {
        var options = new DbContextOptionsBuilder<KanbanContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB for each test
            .Options;
        
        _kanbanContext= new KanbanContext(options);
    }
    [Fact]
    public async Task create_adds_new_card_and_returns_mapped_dto()
    {
        // Arrange
        var service = new KanbanCrudService(_kanbanContext);
        var inputDto = _fixture.Create<KanbanCardDto>();

        // Act
        var result = await service.Create(inputDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(inputDto.Titulo, result.Titulo);
        Assert.Equal(inputDto.Conteudo, result.Conteudo);
        Assert.Equal(inputDto.Lista, result.Lista);
    }

            // Update returns NotFound when card ID doesn't exist
    [Fact]
    public async Task update_returns_notfound_for_nonexistent_card()
    {
        // Arrange
        var service = new KanbanCrudService(_kanbanContext);
        var updateDto = _fixture.Create<KanbanCardDto>();

        // Act
        var result = await service.Update(Guid.NewGuid(), updateDto);

        // Assert
        Assert.True(result.IsT0);
        Assert.IsType<NotFound>(result.AsT0);
    }

        // Delete successfully removes existing card and returns Success
    [Fact]
    public async Task delete_removes_existing_card_and_returns_success()
    {
        
        // Arrange
        var existingCard = _fixture.Build<KanbanCardDto>()
           .Create();
        var service = new KanbanCrudService(_kanbanContext);
        
        var created=await service.Create(existingCard);
        // Act
        var result = await service.Delete(created.Id);

        // Assert
        Assert.True(result.IsT1);
    }
}