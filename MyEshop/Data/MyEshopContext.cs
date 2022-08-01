using Microsoft.EntityFrameworkCore;
using MyEshop.Models;

namespace MyEshop.Data
{
    public class MyEshopContext:DbContext 
    {
        public MyEshopContext(DbContextOptions<MyEshopContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Category
            modelBuilder.Entity<Category>().HasData(new Category()
            {
                ID =1,
                Name="Asp.Net",
                Description= "Asp.Net"
            }, new Category()
            {
                ID = 2,
                Name = "لباس ورزشی",
                Description = "لباس ورزشی"
            }, new Category()
            {
                ID = 3,
                Name = "لوازم خانگی",
                Description = "لوازم خانگی"
            }, new Category()
            {
                ID = 4,
                Name = "ساعت مچی",
                Description = "ساعت مچی"
            }
            ) ;

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
