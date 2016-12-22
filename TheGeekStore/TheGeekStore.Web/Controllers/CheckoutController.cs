using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.ViewModels;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private CartRepository carts;
        private CustomerProfileRepository customerProfiles;
        private PurchaseRepository purchases;

        public CheckoutController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            carts = new CartRepository(context);
            customerProfiles = new CustomerProfileRepository(context);
            purchases = new PurchaseRepository(context);
        }

        public ActionResult ReviewOrder()
        {
            // Get cart
            CartModel cart = carts.FindByUserId(User.Identity.GetUserId());
            return View(cart);
        }

        [HttpGet]
        public ActionResult Shipping()
        {
            CustomerProfileModel profile = customerProfiles.FindByUserId(User.Identity.GetUserId());
            return View(profile);
        }

        [HttpPost]
        public ActionResult Shipping(CustomerProfileModel model)
        {
            return RedirectToAction("Payment");
        }

        [HttpGet]
        public ActionResult Payment()
        {
            PaymentViewModel model = new PaymentViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CheckOutCart");
            }
            return View(model);
        }

        // GET: Checkout
        public ActionResult CheckOutCart()
        {
            CartModel cart = carts.FindByUserId(User.Identity.GetUserId());
            // Create Purchase
            PurchaseModel purchase = new PurchaseModel();
            purchase.PriceShipping = 12.99;
            purchase.PurchaseDate = DateTime.Now;
            purchase.UserId = User.Identity.GetUserId();
            foreach (CartItemModel item in cart.CartItems)
            {
                purchase.PurchaseItems.Add(new PurchaseItemModel
                {
                    ProductId = item.Product.Id,
                    Name = item.Product.Name,
                    ProductNumber = item.Product.ProductNumber,
                    Price = item.Product.Price,
                    Count = item.Count
                });
            }
            purchases.Add(purchase);

            // Delete cart after successful purchase
            carts.Remove(cart);
            return View();
        }


    }
}