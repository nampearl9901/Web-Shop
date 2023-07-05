using ShopCarrs.Models;

namespace ShopCarrs.Repository
{
    public interface IOrderDetailRepository
    {
        public bool Create(OrderDetail orderdetail);
        public bool Update(OrderDetail orderdetail);
        public bool Delete(int orderdetailid);
        public List<OrderDetail> GetAll();
        public OrderDetail findByID(int id);
    }
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private CarShopContext _ctx;
        public OrderDetailRepository(CarShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(OrderDetail orderdetail)
        {
            _ctx.OrderDetails.Add(orderdetail);
            _ctx.SaveChanges();
            return true;
        }
        public bool Delete(int orderdetailid)
        {
            OrderDetail o = _ctx.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderdetailid);
            _ctx.OrderDetails.Remove(o);
            _ctx.SaveChanges();
            return true;
        }
        public List<OrderDetail> GetAll()
        {
            return _ctx.OrderDetails.ToList();

        }
        public bool Update(OrderDetail orderdetail)
        {

            OrderDetail o = _ctx.OrderDetails.FirstOrDefault(x => x.OrderDetailId == orderdetail.OrderDetailId);
            if (o != null)
            {

                _ctx.Entry(o).CurrentValues.SetValues(orderdetail);
                _ctx.SaveChanges();
            }
            return true;
        }
        public OrderDetail findByID(int id)
        {
            OrderDetail o = _ctx.OrderDetails.FirstOrDefault(x => x.OrderDetailId == id);

            return o;
        }

    }
}
