using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using CookingApp.Models;

namespace CookingApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string connectionString;

        public CategoryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = "Select * From Categories";
                return await db.QueryAsync<Category>(query);
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = "Select * From Categories Where Id = @Id";
                return await db.QueryFirstOrDefaultAsync<Category>(query, new { Id = id });
            }
        }

        public async Task<int> CreateCategoryAsync(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = "Insert Into Categories (Name) Values (@Name); Select Cast(Scope_Identity() As Int)";
                return await db.QuerySingleAsync<int>(query, new { category.Name });
            }
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = "Update Categories Set Name = @Name Where Id = @Id";
                var rowsAffected = await db.ExecuteAsync(query, category);
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = "Delete From Categories Where Id = @Id";
                var rowsAffected = await db.ExecuteAsync(query, new { Id = id });
                return rowsAffected > 0;
            }
        }
    }
}
