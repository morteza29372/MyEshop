using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        //baraye upload File
        public IFormFile Picture { get; set; }

        public List<Category> Categories { get; set; }
        public List<int> SelectedGroups { get; set; }
    }
}
