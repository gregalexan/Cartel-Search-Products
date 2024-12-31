namespace Cartel_Search_Products.Models
{
    public class CartModel
    {
        public List<Product> cart { get; set; }

        public double getSubtotal()
        {
            double subtotal = 0;
            foreach (Product product in cart)
            {
                subtotal += product.quantity * product.price;
            }
            return Math.Round(subtotal, 2);
        }

        public double getTax()
        {
            return Math.Round(0.24 * getSubtotal(), 2);
        }

        public double getTotal()
        {
            var tax = getTax();
            var subtotal = getSubtotal();
            return Math.Round(tax + subtotal, 2);
        }
    }
}
