using AZ.Application.Common.Interfaces;
using AZ.Domain.Entities;

namespace AZ.Application.Products.Queries;
public record GetProductQuery(int Id) : IRequest<Product>;
public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
{
    private readonly IApplicationDbContext _context;
    public GetProductQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = _context.Products.Where(p => p.Id == request.Id).FirstOrDefaultAsync();
        return product!;
    }
}
