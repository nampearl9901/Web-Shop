using Microsoft.EntityFrameworkCore;
using ShopCarrs.Models;

namespace ShopCarrs.Repository
{
    public interface IUserRepository    {
        public bool Create(User user);
        public bool CreateUser(User user);
        public bool Update(User user);
        public bool UpdateUser(User user);
        public bool Delete(int userId);
        public List<User> GetAll();
        public User findByID(int id);
        User GetUserByEmail (string email);
    }
    public class UserRepository : IUserRepository
    {
        private CarShopContext _ctx;
        public UserRepository(CarShopContext ctx) {
            _ctx = ctx; }
        public bool Create(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return true;
        }
        public bool CreateUser(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return true;
        }
        public bool Delete(int userId)
        {
            User u = _ctx.Users.FirstOrDefault(x => x.UserId == userId);
            _ctx.Users.Remove(u);
            _ctx.SaveChanges();
            return true;
        }
        public List<User> GetAll()
        {
            return _ctx.Users.ToList();

        }
        
             public bool UpdateUser(User user)
        {
            User u = _ctx.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (u != null)
            {

                _ctx.Entry(u).CurrentValues.SetValues(user);
                _ctx.SaveChanges();
            }
            return true;
        }

        public bool Update(User user)
        {
            User u = _ctx.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (u != null)
            {

                _ctx.Entry(u).CurrentValues.SetValues(user);
                _ctx.SaveChanges();
            }
            return true;
        }
        public User findByID(int id)
        {
            User u = _ctx.Users.FirstOrDefault(x => x.UserId == id);

            return u;
        }
        // Lấy danh sách sản phẩm đã được đặt bởi một người dùng thông qua bảng Carts
        public List<Product> GetAllProductByCarts(int userId)
        {
            return _ctx.Carts
                .Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .Select(c => c.Product)
                .ToList();
        }

        // Lấy danh sách sản phẩm đã được đặt bởi một người dùng thông qua bảng OrderDetails
        public List<Product> GetAllProductByOrderDetails(int userId)
        {
            return _ctx.OrderDetails
                .Include(od => od.Product)
                .Where(od => od.Order.UserId == userId)
                .Select(od => od.Product)
                .ToList();
        }

        public User GetUserByEmail(string email)
        {
            User u = _ctx.Users.FirstOrDefault(x => x.Email == email);
            return u;
        }

    }
}
