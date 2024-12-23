namespace Cartel_Search_Products.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public string category { get; set; }
        public string? description { get; set; }
        public double price { get; set; }
        public int stock { get; set; }
        public int rating { get; set; }
        public string supplier { get; set; }
    }
}
