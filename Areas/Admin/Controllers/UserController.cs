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
    public class UserController : Controller
    {
        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, SignInManager<ApplicationUser> signInManager, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _signInManager = signInManager;

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
        public IActionResult UserView()
        {
            List<User> lst = _userRepository.GetAll();
            return View("UserView", lst);
        }
      
       
    }
}
