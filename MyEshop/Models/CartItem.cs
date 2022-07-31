namespace MyEshop.Models
{
    public class CartItem
    {
        public int ID { get; set; }
        public Item item { get; set; }

        public int Quantity { get; set; }

        public decimal GetTotalPrice()
        {
            return item.Price = Quantity;
        }
    }
}
