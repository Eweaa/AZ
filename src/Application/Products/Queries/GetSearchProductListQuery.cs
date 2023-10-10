using AZ.Application.Common.Interfaces;
using AZ.Domain.Entities;

namespace AZ.Application.Products.Queries;
public record GetSearchProductListQuery(string search) : IRequest<List<Product>>;
public class GetSearchProductListQueryHandler : IRequestHandler<GetSearchProductListQuery, List<Product>>
{
    private readonly IApplicationDbContext _context;
    public GetSearchProductListQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<Product>> Handle(GetSearchProductListQuery request, CancellationToken cancellationToken)
    {
        var result = new List<Product>();
        var products = await _context.Products.ToListAsync();
        
        if(products == null)
        {
            return new List<Product>() { new Product() { Name = "Empty"} };
        }

        foreach (var item in products)
        {
            if (item.Name!.ToLower().Contains(request.search.ToLower()))
            {
                result.Add(item);
            }
        }
        return result;
        
    }
}
