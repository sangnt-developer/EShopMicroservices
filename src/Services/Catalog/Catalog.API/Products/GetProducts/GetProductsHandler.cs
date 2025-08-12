
namespace Catalog.API.Products.GetProducts;

public record GetProductsQuery() : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> products);

public class GetProductsHandler(IDocumentSession session, ILogger<GetProductsHandler> logger)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync();
        return new GetProductsResult(products);
    }
}
