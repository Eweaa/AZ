using AZ.Domain.Entities;

namespace AZ.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }
    DbSet<TodoItem> TodoItems { get; }
    DbSet<Seller> Sellers { get; }
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
