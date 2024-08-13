using MongoDB.Driver;
using NomuBackend.Settings;
using NomuBackend.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NomuBackend.Services
{
    public class ProductService : IProductService // IProductService'i implement etmelisiniz
    {
        private readonly IMongoCollection<Product> _products;

        public ProductService(IMongoClient client, IMongoDbSettings settings)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<List<Product>> GetProductsAsync() =>
            await _products.Find(product => true).ToListAsync();

        public async Task<Product> GetProductByIdAsync(string id) =>
            await _products.Find<Product>(product => product.Id == id).FirstOrDefaultAsync();

        public async Task CreateProductAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateProductAsync(string id, Product productIn) =>
            await _products.ReplaceOneAsync(product => product.Id == id, productIn);

        public async Task RemoveProductAsync(string id) =>
            await _products.DeleteOneAsync(product => product.Id == id);
    }
}
