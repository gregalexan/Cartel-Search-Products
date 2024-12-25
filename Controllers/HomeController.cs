using Cartel_Search_Products.Helpers;
using Cartel_Search_Products.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;

namespace Cartel_Search_Products.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index(string search = "all", string category = "all", string sort = "default", int page = 0, int itemsPerPage = 8)
        {
            // Get all products from the database
            var products = _appDbContext.Product.AsQueryable();

            // If the search keyword is not 'all', perform a search
            if (search != "all")
            {
                // Split the query into words (using space as a delimiter)
                var searchInputs = search.Split(new[] { ' ' });

                // If there are multiple search words, combine them using OR logic
                if (searchInputs.Length > 1)
                {
                    // Use AsEnumerable() to switch to client-side evaluation for the 'Any' condition
                    products = products.AsEnumerable().Where(p =>
                        searchInputs.Any(keyword =>
                            p.productName.Contains(keyword) ||
                            p.category.Contains(keyword) ||
                            p.description.Contains(keyword) ||
                            p.supplier.Contains(keyword)
                        )
                    ).AsQueryable();
                }
                else // single word search
                {
                    products = products.Where(p =>
                        p.productName.Contains(search) ||
                        p.category.Contains(search) ||
                        p.description.Contains(search) ||
                        p.supplier.Contains(search)
                    );
                }
            }


            // Filter by category if specified
            if (category != "all")
            {
                products = products.Where(p => p.category.Equals(category));
            }

            // Sorting logic based on the 'sort' parameter
            products = sort switch
            {
                "price" => products.OrderBy(p => p.price),
                "rating" => products.OrderByDescending(p => 4), // Fix Rating Later TODO!!
                _ => products // Default
            };

            // Pagination logic
            var paginatedProducts = products.Skip(page * itemsPerPage).Take(itemsPerPage).ToList();

            // Pass data to the view
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)products.Count() / itemsPerPage);

            return View("Index", paginatedProducts);
        }

        public IActionResult About()
        {
            return View("About");
        }
    }
}
