using AZ.Application.Common.Interfaces;
using AZ.Domain.Entities;

namespace AZ.Application.Products.Queries;
public record GetCategoryProductsListQuery(int Id) : IRequest<List<Product>>;
public class GetCategoryProductsListQueryHandler : IRequestHandler<GetCategoryProductsListQuery, List<Product>>
{
    private readonly IApplicationDbContext _context;
    public GetCategoryProductsListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public Task<List<Product>> Handle(GetCategoryProductsListQuery request, CancellationToken cancellationToken)
    {
        return _context.Products.Where(p => p.CategoryId == request.Id).ToListAsync();
    }
}
