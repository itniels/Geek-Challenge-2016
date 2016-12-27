using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;

namespace TheGeekStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TheGeekStore.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TheGeekStore.Models.ApplicationDbContext context)
        {
            // Roles
            if (!context.Roles.Any(r => r.Name == "Customer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Customer" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            // Users
            if (!context.Users.Any(u => u.UserName == "admin@tgs.dk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@tgs.dk", Email = "admin@tgs.dk" };

                manager.Create(user, "Admin123");
                manager.AddToRole(user.Id, "Admin");
            }

            if (!context.Users.Any(u => u.UserName == "user@tgs.dk"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "user@tgs.dk", Email = "user@tgs.dk" };

                manager.Create(user, "User123");
                manager.AddToRole(user.Id, "Customer");
            }

            // News
            context.News.AddOrUpdate(
                x => x.Title,
                new NewsModel
                {
                    Title = "Store opened!",
                    Time = new DateTime(2016, 05, 03, 11,25,00),
                    Text = "Today our store opened and it is going to be a wild ride! We are looking very much forward to giving you the very best!"
                },
                new NewsModel
                {
                    Title = "Introducing daily deals!",
                    Time = new DateTime(2016, 08, 03, 11, 25, 00),
                    Text = "Someting very exciting is heppening, we are introducing daily deals! here you will find the very best deals! Every single day there will be new special deals that you just cant beat anywhere else."
                },
                new NewsModel
                {
                    Title = "Raspberry pi is a thing!",
                    Time = new DateTime(2016, 10, 03, 11, 25, 00),
                    Text = "We have expanded our categories, now with a dedicated category for raspberry pi and everything you could possibly imagine for this awesome little IoT device!"
                });


            // Categories
            context.Categories.AddOrUpdate(
                x => x.Name,
                new CategoryModel
                {
                    Name = "Electronics",
                    ImagePath = "/Content/Images/CategoryImages/no_category_image.png",
                    Description = "In this category you will find all kinds of electronic components."
                },
                new CategoryModel
                {
                    Name = "Computers",
                    ImagePath = "/Content/Images/CategoryImages/no_category_image.png",
                    Description = "In this category you will find computers for every hearts desire."
                },
                new CategoryModel
                {
                    Name = "Raspberry Pi",
                    ImagePath = "/Content/Images/CategoryImages/no_category_image.png",
                    Description = "In this category you will find all you would ever need for your Raspberry Pi"
                },
                new CategoryModel
                {
                    Name = "Software",
                    ImagePath = "/Content/Images/CategoryImages/no_category_image.png",
                    Description = "In this category you will find all the software you will need."
                },
                new CategoryModel
                {
                    Name = "Geek Store Gear",
                    ImagePath = "/Content/Images/CategoryImages/no_category_image.png",
                    Description = "In this category you will find all our official 'The Geek Store' gear."
                });

            context.SaveChanges();

            // Products
            context.Products.AddOrUpdate(
                x => x.Name,
                new ProductModel
                {
                    Name = "Raspberry Pi 3",
                    Description = "The brand new Raspberry Pi 3 with even more power than before. Use for all your DIY projects.",
                    Category = context.Categories.Single(x => x.Name == "Raspberry Pi"),
                    ImagePath = "/Content/Images/ProductImages/no_product_image.png",
                    Price = 40.95,
                    ProductNumber = "P20161101",
                    InStock = 11,
                    TimesPuchased = 0,
                    Featured = true
                },

                new ProductModel
                {
                    Name = "MCP3008",
                    Description = "With this chip you can exend the raspberry pi to accuratly read analog inputs.",
                    Category = context.Categories.Single(x => x.Name == "Raspberry Pi"),
                    ImagePath = "/Content/Images/ProductImages/no_product_image.png",
                    Price = 9.99,
                    ProductNumber = "P20161102",
                    InStock = 50,
                    TimesPuchased = 0
                },

                new ProductModel
                {
                    Name = "Windows 10 PRO 64Bit",
                    Description = "The brand new windows 10 PRO 64Bit operating system is the last OS you will ever need need.",
                    Category = context.Categories.Single(x => x.Name == "Software"),
                    ImagePath = "/Content/Images/ProductImages/no_product_image.png",
                    Price = 110.00,
                    ProductNumber = "S20161103",
                    InStock = 1000,
                    TimesPuchased = 0,
                    Featured = true
                },

                new ProductModel
                {
                    Name = "TGS Gamer 16",
                    Description = "Our well known and powerful gaming PCs, want to be ahead of the game? you need a TGS Gamer rig to beat the best!",
                    Category = context.Categories.Single(x => x.Name == "Computers"),
                    ImagePath = "/Content/Images/ProductImages/no_product_image.png",
                    Price = 1899.00,
                    ProductNumber = "C20161003",
                    InStock = 140,
                    TimesPuchased = 0,
                    Featured = true
                },

                new ProductModel
                {
                    Name = "TGS Charge Station",
                    Description = "The new chargestation from TGS will charge up to 10 devices with a whopping 3.5A for each.",
                    Category = context.Categories.Single(x => x.Name == "Electronics"),
                    ImagePath = "/Content/Images/ProductImages/no_product_image.png",
                    Price = 1899.00,
                    ProductNumber = "E20161003",
                    InStock = 50,
                    TimesPuchased = 0
                },

                new ProductModel
                {
                    Name = "TGS Shirt",
                    Description = "Our brand new TGS shirt, these are a limited edition so get them wile they are hot!.",
                    Category = context.Categories.Single(x => x.Name == "Geek Store Gear"),
                    ImagePath = "/Content/Images/ProductImages/no_product_image.png",
                    Price = 49.99,
                    ProductNumber = "G20161003",
                    InStock = 100,
                    TimesPuchased = 0,
                    Featured = true
                }
                );
        }
    }
}
