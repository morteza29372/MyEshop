using System.Collections.Generic;

namespace MyEshop.Models
{
    public class DetailViewModel
    {
        public Product Product { get; set; }

        public List<Category> categories { get; set; }
    }
}
