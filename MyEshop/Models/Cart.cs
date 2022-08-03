using System.Collections.Generic;
using System.Linq;

namespace MyEshop.Models
{
    public class Cart
    {
        public Cart()
        {
            cartItems = new List<CartItem>();
        }
        public int OrderId { get; set; }

        public List<CartItem> cartItems { get; set; }

        public  void AddItem(CartItem item)
        {
            if (cartItems.Exists(i => i.item.ID == item.item.ID))
            {
                cartItems.Find(i => i.item.ID == item.item.ID)
                    .Quantity += 1;
            }
            else
            {
                cartItems.Add(item);
            }

        }

        public  void RemoveItem(int Id)
        {
            var item = cartItems.SingleOrDefault(c => c.item.ID == Id);
            if (item?.Quantity <= 1)
            {
                cartItems.Remove(item);
            }
            else if (item != null)
            {
                item.Quantity -= 1;
            }
               
        }
            
    }
}
