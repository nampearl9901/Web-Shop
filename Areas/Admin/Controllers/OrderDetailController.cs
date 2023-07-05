using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopCarrs.Areas.Identity.Data;
using ShopCarrs.Models;
using ShopCarrs.Repository;
using System.Data;

namespace ShopCarrs.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class OrderDetailController : Controller
    {
        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public OrderDetailController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/Identity/Account/Login");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OrderDetailView()
        {
            List<OrderDetail> lst = _orderDetailRepository.GetAll();
            return View("OrderDetailView", lst);
        }
    }
}
