namespace ShopCarrs.Models
{
    public class ProductDAO
    {
        //
        private CarShopContext db = new CarShopContext();
        //
        public List<Product> GetALLProducts()
        {
            return db.Products.ToList();
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
        public void Update(Product product)
        {
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = db.Products.Find();
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
