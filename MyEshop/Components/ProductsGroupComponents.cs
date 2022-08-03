using Microsoft.AspNetCore.Mvc;
using MyEshop.Data;
using System.Threading.Tasks;

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
            return View("/Views/Components/ProductGroupsComponents.cshtml", _Context.Categories);
        }
    }
}
