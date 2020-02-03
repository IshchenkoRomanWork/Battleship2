using Battleship2.Core.Models;
using Battleship2.MVC.Hubs;
using BattleShip2.BusinessLogic.Models;
using BattleShip2.BusinessLogic.Services;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Battleship2.Tests
{
    class LogicTests
    {
        ActiveGames ActiveGames { get; set; }
        BattleShipHub Hub { get; set; }


        public void Setup()
        {

        }
        public void Test_Correct_Shooting()
        {
            //bool sendCalled = false;;
            //var mockClients = new Mock<IHubCallerClients>();
            //Hub.Clients = mockClients.Object;
            //dynamic all = new ExpandoObject();
            //all.broadcastMessage = new Action<string, string>((name, message) => {
            //    sendCalled = true;
            //});
            //mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            //Hub.Clients.All.SendAsync("TestUser", "TestMessage");
            //Assert.True(sendCalled);
        }


    }

}
