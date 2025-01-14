using KanBanApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanBanApplication.InfraStructure.Persistence;

public class KanbanContext:DbContext
{
    public DbSet<KanbanCard> KanbanCards { get; set; }   
}