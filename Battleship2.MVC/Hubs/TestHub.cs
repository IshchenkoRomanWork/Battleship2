using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battleship2.MVC.Hubs
{
    public class TestHub : Hub
    {
        public void Ready()
        {
            Clients.Client(this.Context.ConnectionId).SendAsync("message", "Hello world!" + new Random().Next().ToString());
        }
    }
}
