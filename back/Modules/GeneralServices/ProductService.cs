using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Back.Models.General;

namespace Back.Modules.GeneralServices
{
    public class ProductService : IProductService
    {
        private readonly IDbConnection _db;

        public ProductService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            const string query = @"
                SELECT * FROM products
                WHERE id = @ProductId AND is_archived = false;
            ";

            return await _db.QueryFirstOrDefaultAsync<Product>(query, new { ProductId = productId });
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            const string query = @"
                SELECT * FROM products
                WHERE is_archived = false;
            ";

            return await _db.QueryAsync<Product>(query);
        }

        public async Task<IEnumerable<Product>> GetByTypeAsync(ProductType productType)
        {
            const string query = @"
                SELECT * FROM products
                WHERE product_type = @ProductType AND is_archived = false;
            ";

            return await _db.QueryAsync<Product>(query, new { ProductType = productType });
        }

        public async Task<int> CreateAsync(Product product)
        {
            const string query = @"
                INSERT INTO products (id,name, description, product_type, is_archived)
                VALUES (@Id,@Name, @Description, @ProductType, false)
                RETURNING id;
            ";

            return await _db.ExecuteScalarAsync<int>(query, product);
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            const string query = @"
                UPDATE products
                SET name = @Name,
                    description = @Description,
                    product_type = @ProductType
                WHERE id = @Id AND is_archived = false;
            ";

            var affected = await _db.ExecuteAsync(query, product);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            const string query = @"
                UPDATE products
                SET is_archived = true
                WHERE id = @ProductId AND is_archived = false;
            ";

            var affected = await _db.ExecuteAsync(query, new { ProductId = productId });
            return affected > 0;
        }
    }
}
