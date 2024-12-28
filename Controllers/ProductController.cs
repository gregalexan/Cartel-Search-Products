using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Cartel_Search_Products.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Details(int productId = 0)
        {
            Product product = null;
            if (productId == 0)
            {
                return View("Details", null);
            }

            // Get the product object
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ProductModel pm = new ProductModel(connection);

            product = pm.getProductById(productId);



            return View("Details", product);
        }
    }
}
