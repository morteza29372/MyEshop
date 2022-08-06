using System.Collections.Generic;
using System.Linq;
using MyEshop.Models;

namespace MyEshop.Data.Repository
{
    

    public interface IGroupRepository
    {
        IEnumerable<Category> GetAllCategories();

        IEnumerable<ShowGroupViewModel> GetGroupForShow();
    }

    

    public class GroupRepository : IGroupRepository
    {
        private MyEshopContext _Context;

        public GroupRepository(MyEshopContext Context)
        {
            _Context = Context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _Context.Categories;
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForShow()
        {
            return _Context.Categories
                .Select(c => new ShowGroupViewModel()
                {
                    GroupId = c.ID,
                    Name = c.Name,
                    ProductCount = _Context.CategoryToProducts.Count(g => g.CategoryID == c.ID)

                }).ToList();
        }
    }
}
