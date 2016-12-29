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
        private ApplicationDbContext context;
        private CartRepository carts;
        private CustomerProfileRepository customerProfiles;
        private PurchaseRepository purchases;
        private ProductRepository products;

        public CheckoutController()
        {
            context = new ApplicationDbContext();
            carts = new CartRepository(context);
            customerProfiles = new CustomerProfileRepository(context);
            purchases = new PurchaseRepository(context);
            products = new ProductRepository(context);
        }

        [HttpGet]
        public ActionResult Shipping()
        {
            CustomerProfileModel profile = customerProfiles.FindByUserId(User.Identity.GetUserId());

            if (profile == null)
            {
                // Create a new profile if none exists
                profile = new CustomerProfileModel();
                profile.UserId = User.Identity.GetUserId();
                customerProfiles.Add(profile);
            }
            return View(profile);
        }

        [HttpPost]
        public ActionResult Shipping(CustomerProfileModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the profile for next time.
                customerProfiles.Edit(model);
                // Return Payment view
                return RedirectToAction("Payment");
            }
            return View(model);

        }

        [HttpGet]
        public ActionResult Payment()
        {
            CartModel cart = carts.FindByUserId(User.Identity.GetUserId());
            if (cart == null)
                return HttpNotFound("Cart was not found...");

            PaymentViewModel model = new PaymentViewModel();
            model.PaymentAmount = cart.GetTotalPrice();

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
            purchase.PurchaseItems = new List<PurchaseItemModel>();
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
                // Update InStock
                item.Product.InStock--;
                products.Edit(item.Product);
                
            }
            purchases.Add(purchase);

            // Delete cart after successful purchase
            carts.Remove(cart);
            return View("Success");
        }

        //public ActionResult Success(int id)
        //{
        //    PurchaseModel purchase = purchases.FindById(id);
        //    // Create an order
            

        //    CartModel cart = carts.FindByUserId(User.Identity.GetUserId());
        //    cart.CartItems.Clear();
        //    carts.Edit(cart);

        //    return View();
        //}


    }
}