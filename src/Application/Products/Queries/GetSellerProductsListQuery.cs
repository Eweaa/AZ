using AZ.Application.Common.Interfaces;
using AZ.Domain.Entities;

namespace AZ.Application.Products.Queries;
public record GetSellerProductsListQuery(int Id) : IRequest<List<Product>>;
public class GetSellerProductsListQueryHandler : IRequestHandler<GetSellerProductsListQuery, List<Product>>
{
    private readonly IApplicationDbContext _context;
    public GetSellerProductsListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<Product>> Handle(GetSellerProductsListQuery request, CancellationToken cancellationToken)
    {
        return _context.Products.Where(p => p.SellerId == request.Id).ToListAsync();
    }
}
