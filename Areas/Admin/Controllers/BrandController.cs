using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopCarrs.Areas.Identity.Data;
using ShopCarrs.Models;
using ShopCarrs.Repository;
using System.Data;

namespace ShopCarrs.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class BrandController : Controller
    {

        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public BrandController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
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
        public IActionResult BrandView()
        {
            List<Brand> lst = _brandRepository.GetAll();
            return View("BrandView", lst);
        }
        [HttpGet]
        public IActionResult CreateBrand()
        {
           
            return View("CreateBrand", new Brand());

        }
        public IActionResult saveBrand(Brand brand)
        {
            

            _brandRepository.Create(brand);
            return RedirectToAction("BrandView", new Brand());

        }
        public IActionResult EditBrand(int id)
        {
            
            return View("EditBrand", _brandRepository.findByID(id));
        }
        [HttpPost]
        public IActionResult UpdateBrand(Brand brand)
        {
            _brandRepository.Update(brand);
            return RedirectToAction("BrandView");
        }
        //delete
        public IActionResult DeleteBrand(int id)
        {
            _brandRepository.Delete(id);
            return RedirectToAction("BrandView");

        }
    }
}
