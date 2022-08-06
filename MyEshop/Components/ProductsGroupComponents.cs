using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Data;
using System.Threading.Tasks;
using MyEshop.Models;

namespace MyEshop.Components
{
    public class ProductsGroupComponents:ViewComponent
    {
        private MyEshopContext _Context;

        public ProductsGroupComponents(MyEshopContext context)
        {
            _Context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {


            var categories = _Context.Categories
                .Select(c => new ShowGroupViewModel()
                {
                    GroupId = c.ID,
                    Name = c.Name,
                    ProductCount = _Context.CategoryToProducts.Count(g => g.CategoryID==c.ID)

                }).ToList();

            return View("/Views/Components/ProductGroupsComponents.cshtml", categories);
        }
    }
}
