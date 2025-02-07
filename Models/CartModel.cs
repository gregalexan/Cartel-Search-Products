namespace Cartel_Search_Products.Models
{
    /* CartModel represents the cart of a user */
    public class CartModel
    {
        public List<Product> cart { get; set; }
        /* 
         * Function to return the current subtotal of the cart.
         * The subtotal is the sum of the prices pre-tax .
        */
        public double getSubtotal()
        {
            double subtotal = 0;
            foreach (Product product in cart)
            {
                subtotal += product.quantity * product.price;
            }
            return Math.Round(subtotal, 2);
        }
        /* Function to return the tax based on the subtotal with 24%. */
        public double getTax()
        {
            return Math.Round(0.24 * getSubtotal(), 2);
        }
        /* Function to return the total value of the cart, after tax. */
        public double getTotal()
        {
            var tax = getTax();
            var subtotal = getSubtotal();
            return Math.Round(tax + subtotal, 2);
        }
    }
}