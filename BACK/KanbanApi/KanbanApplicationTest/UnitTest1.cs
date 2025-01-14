using KanBanApplication.Domain.Entities;
using KanBanApplication.Dtos;
using KanBanApplication.InfraStructure.Persistence;
using KanBanApplication.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using OneOf.Types;

namespace KanbanApplicationTest;

public class UnitTest1
{
    [Fact]
    public async Task create_adds_new_card_and_returns_mapped_dto()
    {
        // Arrange
        var mockContext = new Mock<KanbanContext>();
        var mockSet = new Mock<DbSet<KanbanCard>>();
        mockContext.Setup(c => c.Set<KanbanCard>()).Returns(mockSet.Object);

        var service = new KanbanCrudService(mockContext.Object);
        var inputDto = new KanbanCardInsertDto
        { 
            Titulo = "Test Card",
            Conteudo = "Test Content",
            Lista = "Todo"
        };

        // Act
        var result = await service.Create((KanbanCardDto) inputDto);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<KanbanCard>()), Times.Once);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
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
        var mockContext = new Mock<KanbanContext>();
        var mockSet = new Mock<DbSet<KanbanCard>>();
        mockContext.Setup(c => c.Set<KanbanCard>()).Returns(mockSet.Object);
        mockSet.Setup(s => s.FindAsync(It.IsAny<Guid>())).ReturnsAsync((KanbanCard)null);

        var service = new KanbanCrudService(mockContext.Object);
        var updateDto = new KanbanCardDto
        {
            Id = Guid.NewGuid(),
            Titulo = "Updated Card",
            Conteudo = "Updated Content",
            Lista = "Done"
        };

        // Act
        var result = await service.Update(Guid.NewGuid(), updateDto);

        // Assert
        Assert.True(result.IsT0);
        Assert.IsType<NotFound>(result.AsT0);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

        // Delete successfully removes existing card and returns Success
    [Fact]
    public async Task delete_removes_existing_card_and_returns_success()
    {
        // Arrange
        var mockContext = new Mock<KanbanContext>();
        var mockSet = new Mock<DbSet<KanbanCard>>();
        var cardId = Guid.NewGuid();
        var existingCard = new KanbanCard { Id = cardId };

        mockSet.Setup(m => m.FindAsync(cardId)).ReturnsAsync(existingCard);
        mockContext.Setup(c => c.Set<KanbanCard>()).Returns(mockSet.Object);

        var service = new KanbanCrudService(mockContext.Object);

        // Act
        var result = await service.Delete(cardId);

        // Assert
        mockSet.Verify(m => m.Remove(It.IsAny<KanbanCard>()), Times.Once);
        mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.IsType<Success>(result);
    }
}