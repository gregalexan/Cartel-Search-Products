using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Cartel_Search_Products.Controllers
{
    public class HomeController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product
            {
                ProductID = 1,
                name = "Luxe Loom Towels",
                image = "../Images/bathroom/towels2.png",
                category = "bathroom",
                description = "Nice Towels",
                price = 20.00,
                stock = 40,
                rating = 4,
                supplier = "luxeloom"
            },
            new Product
            {
                ProductID = 2,
                name = "Luxe Loom Towels 2",
                image = "../Images/bathroom/towels3.png",
                category = "bathroom",
                description = "Nice Towels 2",
                price = 10.00,
                stock = 20,
                rating = 3,
                supplier = "luxeloom"
            },
        };

        public IActionResult Index(string search = "all", string category = "all", string sort = "default", int page = 0, int itemsPerPage = 1)
        {
            // Filter Products by search or category
            var filteredProducts = products
            .Where(p => (search == "all" || p.name.Contains(search, System.StringComparison.OrdinalIgnoreCase)) &&
                        (category == "all" || category.Equals("all", System.StringComparison.OrdinalIgnoreCase)))
            .ToList();

            // Sort products
            filteredProducts = sort switch
            {
                "price" => filteredProducts.OrderBy(p => p.price).ToList(),
                "rating" => filteredProducts.OrderByDescending(p => p.rating).ToList(),
                _ => filteredProducts // Default
            };

            // Pagination Logic
            var paginatedProducts = filteredProducts.Skip(page * itemsPerPage).Take(itemsPerPage).ToList();

            // Pass Data to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)System.Math.Ceiling((double)filteredProducts.Count / itemsPerPage);

            return View("Index", paginatedProducts);
        }
    
        public IActionResult About()
        {
            return View("About");
        }
    }
}
