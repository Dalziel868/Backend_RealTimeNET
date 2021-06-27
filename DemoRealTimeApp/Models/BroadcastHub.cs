using DemoRealTimeApp.Models.interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRealTimeApp.Models
{
    public class BroadcastHub:Hub<IHubClient>
    {

    }
}
