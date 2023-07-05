using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCarrs.Models;
using ShopCarrs.Repository;
using System.Data;
using System.Diagnostics;

namespace ShopCarrs.Controllers
{
    [Authorize(Roles = "Customer,Administrator")]
   
    public class HomeController : Controller
    {

        private ProductDAO productDAO = new ProductDAO();


        private IProductRepository _productRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]

        public IActionResult Shop(int id)
        {
            List<Product> ds = productDAO.GetALLProducts();
            return View(ds);

        }
        [AllowAnonymous]
        public IActionResult FindCarByCategoryId(int id)
        {
            List<Product> pr = _productRepository.GetAllProductByCategoryId(id);
            return View(pr);
        }
        [AllowAnonymous]
        public IActionResult FindCarByBrand(int id)
        {
            List<Product> pr = _productRepository.GetAllProductByBrand(id);
            return View(pr);
        }
        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            // Lấy danh sách sản phẩm
            List<Product> lstProducts = _productRepository.GetAll();

            // Tìm sản phẩm cần hiển thị chi tiết
            Product product = _productRepository.findByID(id);

            // Truyền sản phẩm vào View để hiển thị
            return View("Detail", product);
        }
        public IActionResult Cart()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Succes()
        {
            return View();
        }
    }
}