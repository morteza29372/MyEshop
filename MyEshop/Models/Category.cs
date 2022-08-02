using System.Collections;
using System.Collections.Generic;

namespace MyEshop.Models
{
    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<CategoryToProduct> CategoryToProduct { get; set; }
    }
}
