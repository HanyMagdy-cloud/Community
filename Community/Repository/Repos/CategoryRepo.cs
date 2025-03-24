using Community.Repository.Entities;
using Community.Repository.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Community.Repository.Repos
{
    public class CategoryRepo : ICategory
    {
        private readonly string _connString;

        // Constructor to initialize connection string from IConfiguration
        public CategoryRepo(IConfiguration config)
        {
            _connString = config.GetConnectionString("Community");
        }

        // Create a new category
        public Category CreateCategory(Category category)
        {
            using(IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CategoryName", category.CategoryName);

                db.Execute("CreateCategory", parameters,
                    commandType: CommandType.StoredProcedure);

                return category;
            }
        }
        

        // Get all categories
        public List<Category> GetAllCategories()
        {
            using(IDbConnection db =new SqlConnection(_connString))
            {
                db.Open();
                return db.Query<Category>("GetAllCategories", commandType: CommandType.StoredProcedure).AsList();

            }
        }

        // Get a category by its ID
        public Category GetCategoryById(int categoryId)
        {
            using(IDbConnection db = new SqlConnection(_connString))
            {
                db.Open();
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@CategoryId", categoryId);
                return db.QuerySingleOrDefault<Category>("GetCategoryById", parameter, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
