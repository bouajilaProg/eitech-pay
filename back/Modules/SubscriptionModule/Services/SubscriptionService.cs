
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Back.Models.General;
using Dapper;

namespace Back.Modules.SubscriptionModule.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IDbConnection _db;

        public SubscriptionService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            const string query = @"
                SELECT * FROM products
                WHERE id = @ProductId AND is_archived = false AND product_type = @ProductType;
            ";

            return await _db.QueryFirstOrDefaultAsync<Product>(query, new
            {
                ProductId = productId,
                ProductType = ProductType.Subscription.ToString()
            });
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM products
                WHERE is_archived = false AND product_type = @ProductType;
            ";

            return await _db.QueryAsync<Product>(query, new
            {
                ProductType = ProductType.Subscription.ToString()
            });
        }

        public async Task<int> CreateAsync(Product product)
        {
            const string query = @"
                INSERT INTO products (id,name, description, product_type, is_archived)
                VALUES (@Id,@Name, @Description, @ProductType, false)
                RETURNING id;
            ";

            // Force the product type to Subscription
            product.ProductType = ProductType.Subscription.ToString();

            return await _db.ExecuteScalarAsync<int>(query, product);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            const string query = @"
                UPDATE products
                SET name = @Name,
                    description = @Description
                WHERE id = @Id AND is_archived = false AND product_type = @ProductType;
            ";

            product.ProductType = ProductType.Subscription.ToString();

            var affected = await _db.ExecuteAsync(query, product);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            const string query = @"
                UPDATE products
                SET is_archived = true
                WHERE id = @ProductId AND is_archived = false AND product_type = @ProductType;
            ";

            var affected = await _db.ExecuteAsync(query, new
            {
                ProductId = productId,
                ProductType = ProductType.Subscription.ToString()
            });

            return affected > 0;
        }
    }
}
