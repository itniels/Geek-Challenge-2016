using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using TheGeekStore.Core.JsonModels;
using TheGeekStore.Core.Models;
using TheGeekStore.Core.Services;
using TheGeekStore.Hubs;
using TheGeekStore.Models;
using TheGeekStore.Repositories;

namespace TheGeekStore.Controllers
{
    public class CartController : Controller
    {
        private CartRepository carts;
        private ProductRepository products;

        public CartController()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            carts = new CartRepository(context);
            products = new ProductRepository(context);
        }

        public ActionResult ViewCart()
        {
            CartModel model = GetCart();
            return View(model);
        }

        /// <summary>
        /// GET | Adds an item to the cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddToCart(int id)
        {
            ProductModel selectedProduct = products.FindById(id);
            CartModel cart = null;
            if (User.Identity.IsAuthenticated)
            {
                cart = carts.FindByUserId(User.Identity.GetUserId());
            }
            else
            {
                cart = carts.FindById(BrowserStorage.GetCartId(System.Web.HttpContext.Current));
            }

            // See if we have a cart or need a new one
            if (cart == null)
            {
                cart = CreateCart();
            }

            // Add products to cart or update amount
            int count = cart.CartItems.Count(x => x.Product == selectedProduct);
            if (count == 0)
            {
                cart.CartItems.Add(new CartItemModel
                {
                    Product = selectedProduct,
                    Count = 1
                });
            }
            else
            {
                CartItemModel item = cart.CartItems.Single(x => x.Product == selectedProduct);
                item.Count++;
            }
            carts.Edit(cart);

            // Call SignalR
            var context = GlobalHost.ConnectionManager.GetHubContext<ProductPageHub>();
            context.Clients.All.ReadyToBuy(selectedProduct.Id, carts.ProductCountInCarts(selectedProduct));

            return true;
        }

        /// <summary>
        /// GET | JSON | Gets the cart info (Total items and Total Price)
        /// </summary>
        /// <returns></returns>
        public string GetCartInfo()
        {
            var model = new JsonCartInfo();
            CartModel cart = GetCart();

            if (cart != null)
            {
                model.ItemsInCart = cart.GetTotalItemCount();
                model.TotalPrice = cart.GetTotalPrice();
                cart.LastAccessed = DateTime.Now;
                return JsonConvert.SerializeObject(model);
            }
            return null;
        }


        public ActionResult Remove(int id)
        {
            CartModel cart = GetCart();
            List<CartItemModel> matches = new List<CartItemModel>();
            foreach (CartItemModel item in cart.CartItems)
            {
                if (item.Id == id)
                    matches.Add(item);
            }
            foreach (CartItemModel item in matches)
            {
                cart.CartItems.Remove(item);
                carts.Edit(cart);
            }
            return RedirectToAction("ViewCart");
        }

        public string AdjustAmount(int id, int amount)
        {
            CartModel cart = GetCart();
            CartItemModel item = cart.CartItems.Single(x => x.Id == id);
            JsonCartAdjustAmount model = new JsonCartAdjustAmount();
            item.Count += amount;
            if (item.Count <= 0)
            {
                cart.CartItems.Remove(item);
            }
            else
            {
                carts.Edit(cart);
                model.TotalPrice = (item.Product.Price * item.Count);
                model.Count = item.Count;
            }
            model.CartPrice = cart.GetTotalPrice();
            
            return JsonConvert.SerializeObject(model);

        }

        /// <summary>
        /// Gets the Users carts or Anonymous user if not logged in.
        /// </summary>
        /// <returns></returns>
        private CartModel GetCart()
        {
            CartModel cart = null;
            if (User.Identity.IsAuthenticated)
            {
                cart = carts.FindByUserId(User.Identity.GetUserId());
            }
            else
            {
                cart = carts.FindById(BrowserStorage.GetCartId(System.Web.HttpContext.Current));
            }
            if (cart == null)
            {
                cart = new CartModel();
                cart.CartItems = new List<CartItemModel>();
            }
            return cart;
        }

        /// <summary>
        /// Creates a new cart for the User / Anonymous.
        /// </summary>
        /// <returns></returns>
        private CartModel CreateCart()
        {
            CartModel cart = new CartModel();
            cart.LastAccessed = DateTime.Now;
            cart.CartItems = new List<CartItemModel>();
            if (User.Identity.IsAuthenticated)
            {
                cart.UserId = User.Identity.GetUserId();
            }
            carts.Add(cart);
            if (!User.Identity.IsAuthenticated)
            {
                BrowserStorage.SetCartId(System.Web.HttpContext.Current, cart.Id);
            }
            return cart;
        }
    }
}