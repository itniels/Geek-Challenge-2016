using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheGeekStore.Core.Models;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.HangFire
{
    public class Jobs
    {
        private ApplicationDbContext context;
        private CartRepository carts;
        private DailyDealRepository dailyDeals;
        private ProductRepository products;


        public Jobs()
        {
            context = new ApplicationDbContext();
            carts = new CartRepository(context);
            dailyDeals = new DailyDealRepository(context);
            products = new ProductRepository(context);
        }

        public void CleanupCarts()
        {
            List<CartModel> expired = carts.GetExpired(5).ToList();
            foreach (CartModel model in expired)
            {
                context.CartItems.RemoveRange(model.CartItems);
                context.SaveChanges();
            }
            carts.RemoveRange(expired);
        }

        /// <summary>
        /// Updates the daily deals daily automatically.
        /// </summary>
        public void DailyDeals(int numberOfDeals = 3)
        {
            // Settings
            var save = 0.85;    // 15% off

            // Remove old deals
            List<DailyDealModel> currentDeals = dailyDeals.GetAll().ToList();
            foreach (DailyDealModel deal in currentDeals)
            {
                deal.Product.Price = deal.ProductOriginalPrice;
                products.Edit(deal.Product);
            }
            dailyDeals.RemoveRange(currentDeals);

            // Add new deals
            Random r = new Random();
            List<int> usedNumbers = new List<int>();
            List<ProductModel> allProducts = products.GetAll().ToList();
            int count = allProducts.Count;
            for (int i = 0; i < numberOfDeals; i++)
            {
                int rInt = r.Next(0, count - 1);
                int maxTry = 10;
                while (usedNumbers.Contains(rInt) && maxTry >= 0)
                {
                    rInt = r.Next(0, count - 1);
                    maxTry--;
                }
                usedNumbers.Add(rInt);

                try
                {
                    DailyDealModel deal = new DailyDealModel
                    {
                        Product = allProducts[rInt]
                    };

                    deal.ProductOriginalPrice = deal.Product.Price;
                    deal.Product.Price = (deal.Product.Price * save);
                    
                    dailyDeals.Add(deal);
                }
                catch (Exception)
                {
                    if (numberOfDeals < 50)
                        numberOfDeals++;
                }

            }
        }
    }
}