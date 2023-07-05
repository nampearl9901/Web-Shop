using ShopCarrs.Models;

namespace ShopCarrs.Repository
{
    public interface IOrderRepository
    {
        public bool Create(Order order);
        public bool Update(Order order);
        public bool Delete(int orderid);
        public List<Order> GetAll();
        public Order findByID(int id);
    }
    public class OrderRepository : IOrderRepository
    {
        private CarShopContext _ctx;
        public OrderRepository(CarShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Order order)
        {
           
            _ctx.Orders.Add(order);
            _ctx.SaveChanges();
            return true;
        }
        public bool Delete(int orderid)
        {
            Order o = _ctx.Orders.FirstOrDefault(x => x.OrderId == orderid);
            _ctx.Orders.Remove(o);
            _ctx.SaveChanges();
            return true;
        }
        public List<Order> GetAll()
        {
            return _ctx.Orders.ToList();

        }
        public bool Update(Order order)
        {

            Order o = _ctx.Orders.FirstOrDefault(x => x.OrderId == order.OrderId);
            if (o != null)
            {

                _ctx.Entry(o).CurrentValues.SetValues(order);
                _ctx.SaveChanges();
            }
            return true;
        }
       
        public Order findByID(int id)
        {
            Order o  = _ctx.Orders.FirstOrDefault(x => x.OrderId == id);

            return o;
        }
    }
}
