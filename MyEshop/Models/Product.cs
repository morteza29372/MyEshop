using System.Collections.Generic;

namespace MyEshop.Models
{
    public class Product
    {
      
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int ItemID { get; set; }


        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }

        public Item Item { get; set; }

        public List<OrderDetail> orderDetails { get; set; }
    }
}
