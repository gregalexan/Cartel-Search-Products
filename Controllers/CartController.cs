using Microsoft.AspNetCore.Mvc;
using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Cartel_Search_Products.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "Cart";
        private readonly IConfiguration _configuration;
        public CartController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private List<Product> GetCart()
        {
            return HttpContext.Session.Get<List<Product>>(CartSessionKey) ?? new List<Product>();
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            var cartModel = new CartModel
            {
                cart = cart
            };

            return View("ViewCart", cartModel);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var cart = GetCart();

            // Get the product object
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ProductModel pm = new ProductModel(connection);
            var product = pm.getProductById(productId);
            connection.Close();

            if (product != null)
            {
                var existing = false;
                foreach (var p in cart)
                {
                    if (p.ProductID == productId)
                    {
                        var newQuantity = p.quantity + quantity;
                        if (p.stock >= newQuantity)
                        {
                            p.quantity += quantity;
                        }
                        else
                        {
                            TempData["Error"] = "You chose more than the available stock!";
                            return RedirectToAction("Details", "Product", new { productId = product.ProductID });
                        }
                        existing = true;
                    }
                }
                if (!existing)
                {
                    product.quantity = quantity;
                    cart.Add(product);
                }
                HttpContext.Session.Set(CartSessionKey, cart);
            }

            TempData["Success"] = $"Product: {product.productName} added to cart successfully!";
            return RedirectToAction("Details", "Product", new { productId = product.ProductID });
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var product = cart.FirstOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                cart.Remove(product);
                HttpContext.Session.Set(CartSessionKey, cart);
                product.quantity = 0;
            }

            TempData["Success"] = "Product: " + product.productName + " deleted successfully!";
            return RedirectToAction("Details", "Product", new { productId = product.ProductID});
        }


    }
}
