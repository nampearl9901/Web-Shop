using ShopCarrs.Models;

namespace ShopCarrs.Repository
{
    public interface IBrandRepository
    {
        public bool Create(Brand brand);
        public bool Update(Brand brand);
        public bool Delete(int brandId);
        public List<Brand> GetAll();
        public Brand findByID(int id);
    }
    public class BrandRepository : IBrandRepository
    {
        private CarShopContext _ctx;
           public BrandRepository(CarShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Brand brand)
        {
            _ctx.Brands.Add(brand);
            _ctx.SaveChanges();
            return true;
        }
        public bool Delete(int brandId)
        {
            Brand p = _ctx.Brands.FirstOrDefault(x => x.BrandId == brandId);
            _ctx.Brands.Remove(p);
            _ctx.SaveChanges();
            return true;
        }
        public List<Brand> GetAll()
        {
            return _ctx.Brands.ToList();

        }
        public bool Update(Brand brand)
        {
            Brand b = _ctx.Brands.FirstOrDefault(x => x.BrandId == brand.BrandId);
            if (b != null)
            {

                _ctx.Entry(b).CurrentValues.SetValues(brand);
                _ctx.SaveChanges();
            }
            return true;
        }
        public Brand findByID(int id)
        {
            Brand b = _ctx.Brands.FirstOrDefault(x => x.BrandId == id);

            return b;
        }
    }
}
