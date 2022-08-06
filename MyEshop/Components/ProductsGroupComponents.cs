using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Data;
using System.Threading.Tasks;
using MyEshop.Data.Repository;
using MyEshop.Models;

namespace MyEshop.Components
{
    public class ProductsGroupComponents:ViewComponent
    {
        private IGroupRepository _getGroupRepository;

        public ProductsGroupComponents(IGroupRepository getGroupRepository)
        {
            _getGroupRepository = getGroupRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/Components/ProductGroupsComponents.cshtml",_getGroupRepository.GetGroupForShow());
        }
    }
}
