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
    public class CategoryController : Controller
    {

        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public CategoryController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
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
        public IActionResult CategoryView()
        {
            List<Category> lst = _categoryRepository.GetAll();
            return View("CategoryView", lst);
        }
        public IActionResult CreateCategory()
        {

            return View("CreateCategory", new Category());

        }
        public IActionResult saveCategory(Category category)
        {


          _categoryRepository.Create(category);
            return RedirectToAction("CategoryView", new Category());

        }
        public IActionResult DeleteCategory(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("CategoryView");

        }
    }
}
