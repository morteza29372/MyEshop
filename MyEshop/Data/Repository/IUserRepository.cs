using Microsoft.EntityFrameworkCore;
using MyEshop.Models;
using System.Linq;

namespace MyEshop.Data.Repository
{
    public interface IUserRepository
    {
        bool IsExistByEmail(string Email);

        void AddUser(Users user);

        Users GetUserForLogin(string Email, string password);
    }

    
    public class UserRepository : IUserRepository
    {
        private MyEshopContext _Context;

        public UserRepository(MyEshopContext context)
        {
            _Context = context;
        }

        public void AddUser(Users user)
        {
            _Context.Add(user);
            _Context.SaveChanges();
        }

        public Users GetUserForLogin(string Email, string password)
        {
            return _Context.Users.SingleOrDefault(u => u.Email == Email && u.Password == password);
        }

        public bool IsExistByEmail(string Email)
        {
            return _Context.Users.Any(u=> u.Email==Email);
        }
    }
}
