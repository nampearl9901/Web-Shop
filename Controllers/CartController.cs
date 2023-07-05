using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopCarrs.Models;
using ShopCarrs.Repository;
using System.Data;

namespace ShopCarrs.Controllers
{
    [Authorize(Roles = "Customer,Administrator")]

    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;




        //
        public CartController(IProductRepository productRepository, IUserRepository userRepository, IOrderDetailRepository orderDetailRepository, IOrderRepository orderRepository)
        {

            _productRepository = productRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        //
        public IActionResult Index()
        {
            ViewBag.sessionId = HttpContext.Session.Id;
            CartModel cartmodel = new CartModel();
            cartmodel.CartId = HttpContext.Session.Id;
            if(HttpContext.Session.Get<List<CartItem>>("cart")!=null )
            {
                List<CartItem>? cartItems = HttpContext.Session.Get<List<CartItem>>("cart");
                cartmodel.setAllItems(cartItems);
            }
            if (cartmodel.getAllItems().Count == 0)
            {
                ViewBag.Message = "Không có sản phẩm nào trong giỏ hàng";
            }
            return View(cartmodel);
        }
        /// add to cart
       
        public IActionResult AddToCart(int id)
        {
            Product product = _productRepository.GetProductById(id);
            int quantity = 1;
            CartModel cartModel = null;


            if (HttpContext.Session.Get<List<CartItem>>("cart") != null)
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<CartItem>>("cart"));
            }
            else
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
            }
            CartItem cartitem = new CartItem()
            {
                Id = product.ProductId,
                Name = product.ProductName,
                Price = (decimal)product.Price,
                ImagePath = product.Image,
                Quantity = quantity,
                lineTotal = (decimal)(product.Price * quantity),
            };

            cartModel.addItem(cartitem);
            //save to session
            HttpContext.Session.Set<List<CartItem>>("cart", cartModel.getAllItems());
            return RedirectToAction("Index");
        }
        //
        public IActionResult UpdateQuantity()
        {
            var btn = Request.Form["btnUpdateQuantity"].ToString();
            var id = Request.Form["item.Id"].ToString();
            int productId = int.Parse(id);
            var qty = Request.Form["item.Quantity"].ToString();
            CartModel cartModel = null;
            if (HttpContext.Session.Get<List<CartItem>>("cart") != null)
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<CartItem>>("cart"));
            }
            cartModel.UpdateQuantity(productId, 1, btn);
            HttpContext.Session.Set<List<CartItem>>("cart", cartModel.getAllItems());
            return RedirectToAction("Index");
        }
       
        public IActionResult RemoveFromCart(int productId)
        {
            CartModel cartModel = null;
            if (HttpContext.Session.Get<List<CartItem>>("cart") != null)
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<CartItem>>("cart"));
            }
            cartModel.RemoveItem(productId);
            HttpContext.Session.Set<List<CartItem>>("cart", cartModel.getAllItems());
            return RedirectToAction("Index");
        }
        /// <summary>
        /// \ [HttpPost]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Success()
        {
            List<CartItem>? items = HttpContext.Session.Get<List<CartItem>>("cart");
            CartModel cartModel = new CartModel();
            cartModel.setAllItems(items);
            var name = Request.Form["Name"].ToString();
            var email = Request.Form["Email"].ToString();
            var address = Request.Form["Address"].ToString();
            var phone = Request.Form["Phone"].ToString();
            var gender = Request.Form["Gender"].ToString();
            if (_userRepository.GetUserByEmail(email) == null)
            {
                User user = new User();
                user.FullName = name;
                user.Email = email;
                user.Address = address;
                user.Phone = phone; 
                user.Gender = gender;
               _userRepository.CreateUser(user);
            }
            else
            {
                User user = _userRepository.GetUserByEmail(email);
                user.FullName = name;
                user.Email = email;
                user.Address = address;
                user.Phone = phone;
                user.Gender = gender;
                _userRepository.CreateUser(user);
            }
            // 1 => shoppingcart
            OrderSuccess orderSuccess = new OrderSuccess();
            orderSuccess.FullName = name;
            orderSuccess.Email = email;
            orderSuccess.Address = address;
            orderSuccess.Phone = phone;
            orderSuccess.Gender = gender;
            orderSuccess.Total = (int)cartModel.getTotal();
            orderSuccess.OrderDate = DateTime.Now;
            
            // 2 => insert order
            Order order = new Order();
            order.UserId = _userRepository.GetUserByEmail(email).UserId;
            order.OrderDate = DateTime.Now;
            order.TotalAmount = (int)cartModel.getTotal();
            _orderRepository.Create(order);
            foreach (var item in items)
            {
                // 3 => insert orderdetails
                OrderDetail orderDetail = new OrderDetail();
              
                orderDetail.ProductId = item.Id;
                orderDetail.Quantity = item.Quantity;
                orderDetail.Price = item.Price;
                orderDetail.OrderId = order.OrderId;
                _orderDetailRepository.Create(orderDetail);
            }
            HttpContext.Session.Remove("cart");
            return View("success", orderSuccess);
        }
        public ActionResult Checkout()
        {
            List<CartItem>? items = HttpContext.Session.Get<List<CartItem>>("cart");
            CartModel cartModel = new CartModel();
            cartModel.setAllItems(items);
            // 1 => shoppingcart
            // 2 => insert order
            foreach (var item in items)
            {

                // 3 => insert orderdetails
            }
            return View(cartModel);
        }


    }
}





