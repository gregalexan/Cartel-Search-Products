using Cartel_Search_Products.Models;

namespace Cartel_Search_Products.Services
{
    public class ProductService
    {
        public void SetProductRating(Product product)
        {
            // For now 
            if (product.price > 12)
            {
                product.rating = 5;
            }
            else
            {
                product.rating = 4;
            }
        }
    }
}
