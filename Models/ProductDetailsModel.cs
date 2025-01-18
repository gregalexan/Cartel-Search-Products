namespace Cartel_Search_Products.Models
{
    /* Custom object to pass to Details.cshtml
       Makes it easier to show the results 
    */
    public class ProductDetailsModel
    {
        public Product Product { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Product> Related {  get; set; }
    }
}
