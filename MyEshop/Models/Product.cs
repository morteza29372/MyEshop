using System.Collections.Generic;

namespace MyEshop.Models
{
    public class Product
    {
        public Product()
        {
            Categories = new List<Category>();
        }
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; }
    }
}
