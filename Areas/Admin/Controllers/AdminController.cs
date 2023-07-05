using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopCarrs.Models;
using ShopCarrs.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ShopCarrs.Areas.Identity.Data;

namespace ShopCarrs.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
   

    public class AdminController : Controller
    {
        private IProductRepository _productRepository;
        private IBrandRepository _brandRepository;
        private ICategoryRepository _categoryRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminController(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, SignInManager<ApplicationUser> signInManager)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
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
     
        public IActionResult ProductView()
        {
            List<Product> lst = _productRepository.GetAll();
            return View("ProductView", lst);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            var q1 = from b in _brandRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = b.BrandName,
                         Value = b.BrandId.ToString()

                     };
            var q2 = from c in _categoryRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.CategoryName,
                         Value = c.CategoryId.ToString()
                     };

            ViewBag.BrandId = q1.ToList();
            ViewBag.CategoryId = q2.ToList();
            return View("CreateProduct", new Product());

        }
        [HttpPost]
        public IActionResult saveProduct(Product product)
        {
            //if (ModelState.IsValid)
            //{
            //    bool isProductNameExist = _productRepository.checkName(product.ProductName);
            //    if (isProductNameExist)
            //    {
            //        ModelState.AddModelError(string.Empty, "tên đã có");

            //        return View("CreateProduct");


            //    }
            //    _productRepository.Create(product);

            //    return RedirectToAction("ProductView");

            //}
            //else
            //{
            //    return View("CreateProduct");
            //}

            _productRepository.Create(product);
            return RedirectToAction("ProductView", new Product());

        }


        //edit
        public IActionResult EditProduct(int id)
        {
            var q1 = from b in _brandRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = b.BrandName,
                         Value = b.BrandId.ToString()

                     };
            var q2 = from c in _categoryRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.CategoryName,
                         Value = c.CategoryId.ToString()
                     };

            ViewBag.BrandId = q1.ToList();
            ViewBag.CategoryId = q2.ToList();
            return View("EditProduct", _productRepository.findByID(id));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            return RedirectToAction("ProductView");
        }
        //delete
        public IActionResult DeleteProduct(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("ProductView");

        }
    }


}
