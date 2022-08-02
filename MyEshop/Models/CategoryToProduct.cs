using Microsoft.EntityFrameworkCore;

namespace MyEshop.Models
{
    
    public class CategoryToProduct
    {
        public int  CategoryID  { get; set; }

        public int ProductID { get; set; }


        //Navigation Property
        public Category Category { get; set; }

        public Product Product { get; set; }
    }

    
}
