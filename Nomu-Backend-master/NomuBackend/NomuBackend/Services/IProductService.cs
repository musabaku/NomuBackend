namespace NomuBackend.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(string id, Product productIn);
        Task RemoveProductAsync(string id);
    }
}
