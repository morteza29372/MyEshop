﻿using Microsoft.EntityFrameworkCore;
using MyEshop.Models;

namespace MyEshop.Data
{
    public class MyEshopContext:DbContext 
    {
        public MyEshopContext(DbContextOptions<MyEshopContext> options):base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CategoryToProduct> CategoryToProducts { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet <Order>  Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryToProduct>().
                HasKey(t => new { t.ProductID, t.CategoryID });

            modelBuilder.Entity<Item>(i =>
            {
                i.Property(w => w.Price).HasColumnType("Money");
                i.HasKey(w => w.ID);
            });

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
            modelBuilder.Entity<Item>().HasData(
               new Item()
               {
                   ID = 1,
                   Price = 854.0M,
                   QuantityInStock = 5
               },
           new Item()
           {
               ID = 2,
               Price = 3302.0M,
               QuantityInStock = 8
           },
           new Item()
           {
               ID = 3,
               Price = 2500,
               QuantityInStock = 3
           });

            modelBuilder.Entity<Product>().HasData(new Product()
            {
                ID = 1,
                ItemID = 1,
                Name = "آموزش Asp.Net Core 3 پروژه محور",
                Description =
                        "آموزش Asp.Net Core 3 پروژه محور ASP.NET Core بر پایه‌ی NET Core.استوار است و نگارشی از NET.محسوب می شود که مستقل از سیستم عامل و بدون واسط برنامه نویسی ویندوز عمل می کند.ویندوز هنوز هم سیستم عاملی برتر به حساب می آید ولی برنامه های وب نه تنها روز به روز از کاربرد و اهمیت بیشتری برخوردار می‌شوند بلکه باید بر روی سکوهای دیگری مانند فضای ابری(Cloud) هم بتوانند میزبانی(Host) شوند، مایکروسافت با معرفی ASP.NET Core گستره کارکرد NET.را افزایش داده است.به این معنی که می‌توان برنامه‌های کاربردی ASP.NET Core را بر روی بازه‌ی گسترده ای از محیط‌های مختلف میزبانی کرد هم‌اکنون می‌توانید پروژه های وب را برای Linux یا macOS هم تولید کنید."
            },
                new Product()
                {
                    ID = 2,
                    ItemID = 2,
                    Name = "آموزش Blazor از مقدماتی تا پیشرفته",
                    Description =
                        "در سال های گذشته ، کمپانی مایکروسافت با معرفی تکنولوژی های جدید و حرفه ای در زمینه های مختلف ، عرصه را برای سایر کمپانی ها تنگ تر کرده است. اخیرا این غول فناوری با معرفی فریم ورک های ASP.NET Core و همینطور Blazor قدرت خود در زمینه ی وب را به اثبات رسانده است."
                },
                new Product()
                {
                    ID = 3,
                    ItemID = 3,
                    Name = "آموزش اپلیکیشن های وب پیش رونده ( PWA )",
                    Description = "آموزش اپلیکیشن های وب پیش رونده ( PWA ) آموزش PWA از مقدماتی تا پیشرفته وب اپلیکیشن‌های پیش رونده(PWA) نسل جدید اپلیکیشن‌های تحت وب هستند که می‌توانند آینده‌ی اپلیکیشن‌های موبایل را متحول کنند.در این دوره به طور جامع به بررسی آن‌ها خواهیم پرداخت. مزایا و فاکتور هایی که یک وب اپلیکیشن دارا می باشد به صورت زیر می باشد : ریسپانسیو :  رکن اصلی سایت برای PWA ریسپانسیو بودن اپلیکیشن هستش که برای صفحه نمایش کاربران مختلف موبایل و تبلت خود را وفق دهند."
                });

            modelBuilder.Entity<CategoryToProduct>().HasData(
                new CategoryToProduct() { CategoryID = 1, ProductID = 1 },
                new CategoryToProduct() { CategoryID = 2, ProductID = 1 },
                new CategoryToProduct() { CategoryID = 3, ProductID = 1 },
                new CategoryToProduct() { CategoryID = 4, ProductID = 1 },
                new CategoryToProduct() { CategoryID = 1, ProductID = 2 },
                new CategoryToProduct() { CategoryID = 2, ProductID = 2 },
                new CategoryToProduct() { CategoryID = 3, ProductID = 2 },
                new CategoryToProduct() { CategoryID = 4, ProductID = 2 },
                new CategoryToProduct() { CategoryID = 1, ProductID = 3 },
                new CategoryToProduct() { CategoryID = 2, ProductID = 3 },
                new CategoryToProduct() { CategoryID = 3, ProductID = 3 },
                new CategoryToProduct() { CategoryID = 4, ProductID = 3 }
                );

            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
