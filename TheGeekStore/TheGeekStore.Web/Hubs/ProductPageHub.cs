using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using TheGeekStore.Core.SignalRModels;

namespace TheGeekStore.Hubs
{
    public class ProductPageHub : Hub
    {
        private static List<UserTrackingModel> Users = new List<UserTrackingModel>();

        // Overridable hub methods  
        public override Task OnDisconnected(bool stopCalled)
        {
            var uid = Context.ConnectionId;
            UserTrackingModel user = Users.Find(x => x.UserId == uid);
            var productId = user.ProductId;
            Users.Remove(user);
            CurrentlyViewing(productId);
            return null;
        }

        // Listeners
        public void CurrentlyViewing(int id)
        {
            int count =  Users.Count(x => x.ProductId == id);
            Clients.All.CurrentlyViewing(id, count);
        }

        public void TimesPurchased(int id, int purchased, int inStock)
        {
            Clients.All.TimesPurchased(id, purchased, inStock);
        }

        public void ReadyToBuy(int id, int count)
        {
            Clients.All.ReadyToBuy(id, count);
        }

        // User Actions
        public void LoadedPage(int id)
        {
            var uid = Context.ConnectionId;
            UserTrackingModel user = Users.Find(x => x.UserId == uid);
            if (user == null)
            {
                user = new UserTrackingModel {UserId = uid, ProductId = id};
                Users.Add(user);
            }
            else
            {
                user.ProductId = id;
            }

            CurrentlyViewing(id);
        }
    }
}