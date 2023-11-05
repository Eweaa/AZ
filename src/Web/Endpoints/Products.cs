using AZ.Application.Products.Queries;
using AZ.Domain.Entities;

namespace AZ.Web.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetCategoryProducts, "Category/{Id}")  
            .MapGet(GetSellerProducts, "Seller/{Id}")  
            .MapGet(GetProduct, "Product/{Id}")  
            .MapGet(GetSearchProducts, "{Search}");  
    }

    public async Task<List<Product>> GetCategoryProducts(ISender sender, [AsParameters] GetCategoryProductsListQuery query)
    {
        return await sender.Send(query);
    }
    
    public async Task<List<Product>> GetSellerProducts(ISender sender, [AsParameters] GetSellerProductsListQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<Product> GetProduct(ISender sender, [AsParameters] GetProductQuery query)
    {
        return await sender.Send(query);
    }
    
    public async Task<List<Product>> GetSearchProducts(ISender sender, [AsParameters] GetSearchProductListQuery query)
    {
        return await sender.Send(query);
    }
}
