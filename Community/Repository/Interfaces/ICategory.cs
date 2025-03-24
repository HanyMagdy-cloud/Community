using Community.Repository.Entities;

namespace Community.Repository.Interfaces
{
    //ICategory interface, which will include the methods for interacting with the categories.
    public interface ICategory
    {
        public Category CreateCategory(Category category);
        public Category GetCategoryById(int categoryId);
        public List<Category> GetAllCategories();
        
    }
}
