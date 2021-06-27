using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRealTimeApp.Models.interfaces
{
    public interface IHubClient
    {
        Task BroadCastMessage();
    }
}
