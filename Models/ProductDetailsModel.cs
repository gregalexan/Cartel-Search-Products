namespace Cartel_Search_Products.Models
{
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Product> Related {  get; set; }
    }
}
