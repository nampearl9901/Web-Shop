using ShopCarrs.Models;

namespace ShopCarrs.Repository
{
    public interface ICategoryRepository
    {
        public bool Create(Category category);
        public bool Update(Category category);
        public bool Delete(int categoryId);
        public List<Category> GetAll();
        public Category findByID(int categoryId);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private CarShopContext _ctx;
        public CategoryRepository(CarShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Category category)
        {
            _ctx.Categories.Add(category);
            _ctx.SaveChanges();
            return true;
        }
        public bool Delete(int categoryId)
        {
            Category c = _ctx.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            _ctx.Categories.Remove(c);
            _ctx.SaveChanges();
            return true;
        }
        public List<Category> GetAll()
        {
            return _ctx.Categories.ToList();

        }
        public bool Update(Category category)
        {
            Category c = _ctx.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
            if (c != null)
            {

                _ctx.Entry(c).CurrentValues.SetValues(category);
                _ctx.SaveChanges();
            }
            return true;
        }
        public Category findByID(int categoryId)
        {
            Category c = _ctx.Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            return c;
        }
    }
}
