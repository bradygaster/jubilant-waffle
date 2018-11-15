
using System;
using System.Linq;
using DispatchR.Data.Models;
using DispatchR.Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace DispatchR.Hubs
{
    public class DispatchRHub : Hub
    {
        public DispatchRHub(PlacesContext context)
        {
            PlacesContext = context;
        }

        private PlacesContext PlacesContext { get; }

        public void RequestAssistance(Guid placeId)
        {
            var place = PlacesContext.Places.First(x => x.Id == placeId).CreateViewModel();
            Clients.Others.SendCoreAsync("assistanceRequested", new object[] { place });
        }
    }
}