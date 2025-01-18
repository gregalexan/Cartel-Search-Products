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
        /* Redirect to product details based on the product user chose on the home page */
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
            connection.Close();

            if (product == null)
            {
                return View("Details", null);
            }

            // Get the reviews of the product
            using var connection1 = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ReviewModel rm = new ReviewModel(connection1);
            List<Review> reviews = rm.getProductReviews(product);
            connection1.Close();

            // Get the related products of this product
            // related products are the products with the same category
            List<String> keywords = new List<String>();
            keywords.Add(product.category);
            using var connection2 = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ProductModel productModel = new ProductModel(connection2);
            List<Product> related = productModel.viewProducts(keywords, true);
            connection2.Close();

            // exclude from the related list the current product
            foreach (var prod in related)
            {
                if (prod.ProductID == productId)
                {
                    related.Remove(prod);
                    break;
                }
            }
            /* I think it is easier with this custom model */
            var productDetails = new ProductDetailsModel
            {
                Product = product,
                Reviews = reviews,
                Related = related
            };

            return View("Details", productDetails);
        }
        /* Redirects to the product full reviews view based on the product user chose */
        public IActionResult Reviews(int productId = 0)
        {
            if (productId == 0)
            {
                return View("Reviews", null);
            }

            // Get Product By id
            Product product;
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ProductModel pm = new ProductModel(connection);
            product = pm.getProductById(productId);
            connection.Close();

            if (product == null)
            {
                return View("Reviews", null);
            }

            // Get the Reviews
            using var connection1 = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ReviewModel rm = new ReviewModel(connection1);
            List<Review> reviews = rm.getProductReviews(product);
            connection1.Close();

            ProductDetailsModel productDetails = new ProductDetailsModel
            {
                Product = product,
                Reviews = reviews
            };

            return View("Reviews", productDetails);
        }
        /* Details page, user clicks to add a review -> redirect to AddReview view */
        public IActionResult AddReview(int productId = 0)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["ErrorMessage"] = "You have to log in to post a review";
                return RedirectToAction("Join", "Login"); // Redirect to the login page
            }
            if (productId == 0)
            {
                return View("AddReview", null);
            }
            // Get Product By id
            Product product;
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ProductModel pm = new ProductModel(connection);
            product = pm.getProductById(productId);
            connection.Close();

            if (product == null)
            {
                return View("AddReview", null);
            }

            return View("AddReview", product);
        }
        /* Get the inputs from the review form and call the ReviewModel to post it to the database */
        public IActionResult PostReview(int productID = 0, string companyName = null, int stars = 0, string review = "")
        { 
            if (productID == 0 || companyName == null || stars == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            Review rev = new Review
            {
                productID = productID,
                companyName = companyName,
                stars = stars,
                review = review
            };

            Product product = null;

            // Get the product object
            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ReviewModel rm = new ReviewModel(connection);
            rm.postReview(rev);

            return RedirectToAction("Details", "Product", new { productid = rev.productID });
        }
    }
}
