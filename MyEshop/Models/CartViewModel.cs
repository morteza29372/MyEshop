using System.Collections.Generic;

namespace MyEshop.Models
{
    public class CartViewModel
    {

        public CartViewModel()
        {
            cartItems = new List<CartItem>();
        }
        public List<CartItem> cartItems { get; set; }

        public decimal OrderTotal { get; set; }
    }
}
