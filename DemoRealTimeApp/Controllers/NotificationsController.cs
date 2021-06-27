using DemoRealTimeApp.Data;
using DemoRealTimeApp.Models;
using DemoRealTimeApp.Models.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRealTimeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IHubContext<BroadcastHub, IHubClient> _hubContext;
        public NotificationsController(MyDbContext context, IHubContext<BroadcastHub, IHubClient> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        [Route("notificationcount")]
        [HttpGet]
        public async Task<ActionResult<NotificationCountResult>> GetNotificationCount()
        {
            var count = await _context.Notifications.CountAsync();
            NotificationCountResult notifyCount = new NotificationCountResult()
            {
                Count = count
            };
            return notifyCount;
        }
        [Route("notificationresult")]
        [HttpGet]
        public async Task<ActionResult<List<NotificationResult>>> GetNotificationMessage()
        {
            var emplist = from mess in _context.Notifications
                          orderby mess.Id descending
                          select new NotificationResult
                          {
                              EmployeeName = mess.EmployeeName,
                              TranType = mess.TranType
                          };
            return await emplist.ToListAsync();
        }

        [Route("deletenotifications")]
        [HttpDelete]
        public async Task<IActionResult> DeleteNotifications()
        {
            await _context.Database.ExecuteSqlRawAsync("truncate table Notifications");
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.BroadCastMessage();
            return NoContent();
        }
    }
}
