using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Cartel_Search_Products.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /* Fetches the products from the database and redirects user to homepage 
           Also, filters the products by search queries, category, sorting.
           The Pagination logic happens here
        */
        [HttpGet]
        [HttpPost]
        public IActionResult Index(string search = "all", string category = "all", string sort = "default", int page = 0, int itemsPerPage = 8)
        {
            // A list of strings (keywords) based on which products will be shown
            List<String> keywords = new List<String>();

            // view products by category
            Boolean categoryFlag = false;
            if (category != "all")
            {
                keywords.Add(category);
                categoryFlag = true;
            }

            if (search != "all")
            {
                // Split the search query on every space and add them in the keywords
                String[] words = search.Split("\\s+");
                foreach (String word in words)
                {
                    keywords.Add(word.Trim());
                }
            }

            // Neither category is selected nor a search query is provided
            if (keywords.Count == 0)
            {
                keywords.Add("all");
            }

            using var connection = new MySqlConnection(_configuration.GetConnectionString("Default"));
            ProductModel pm = new ProductModel(connection);
            var products_before = pm.viewProducts(keywords, categoryFlag);

            var products_sort = pm.SortProducts(products_before, sort);

            //Pagination logic
            var products = products_sort.Skip(page * itemsPerPage).Take(itemsPerPage).ToList();

            // Pass data to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)products_sort.Count() / itemsPerPage);

            connection.Close();


            return View("Index", products);
        }
        /* Redirect user to about cartel page */
        public IActionResult About()
        {
            return View("About");
        }
    }
}
