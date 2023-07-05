using ShopCarrs.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ShopCarrs.Repository
{
    public interface IProductRepository
    {
        public bool Create(Product product);
        public bool Update(Product product);
        public bool Delete(int productId);
        public List<Product> GetAll();
        public Product findByID(int id);
        public bool checkName(string name);
        public List<Product> GetAllProductByCategoryId(int id);
        public List<Product> GetAllProductByBrand(int id);

        public Product GetProductById(int id);
        
    }
    public class ProductReposistory : IProductRepository
    {
        private CarShopContext _ctx;
        public ProductReposistory(CarShopContext ctx)
        {
            _ctx = ctx;
        }

        public bool checkName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return _ctx.Products.Any(x => x.ProductName.Trim().Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
        }
        public bool Create(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return true;
        }
        public bool Delete(int productId)
        {
            Product p = _ctx.Products.FirstOrDefault(x => x.ProductId==productId);
            _ctx.Products.Remove(p);
            _ctx.SaveChanges();
            return true;
        }
        public List<Product> GetAll()
        {
            return _ctx.Products.ToList();

        }
        public bool Update(Product product)
        {
            Product p = _ctx.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (p != null) {
            
            _ctx.Entry(p).CurrentValues.SetValues(product);
                _ctx.SaveChanges() ;
            }
            return true;
        }
        public Product findByID(int id)
        {
            Product p = _ctx.Products.FirstOrDefault(x => x.ProductId==id);
           
            return p;
        }
        public List<Product> GetAllProductByCategoryId(int id)
        {
            return _ctx.Products.Where(x => x.CategoryId == id).ToList();
        }
        public List<Product> GetAllProductByBrand(int id)
        {
            return _ctx.Products.Where(x => x.BrandId == id).ToList();  
        }
        public Product GetProductById(int id)
        {
            Product c = _ctx.Products.FirstOrDefault(x => x.ProductId == id);
            return c;
        }

    }
}
