using System.ComponentModel.DataAnnotations.Schema;

namespace Cartel_Search_Products.Models
{
    /* Model to represent the products */
    public class Product
    {
        public int ProductID { get; set; }
        public string productName { get; set; }
        public string? image { get; set; }
        public string category { get; set; }
        public string? description { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public string supplier { get; set; }
        
        [NotMapped]
        public int rating { get; set; }

        [NotMapped]
        public int quantity { get; set; } // The existing quantity in the cart
    }
}
