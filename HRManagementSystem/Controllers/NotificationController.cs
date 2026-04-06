using CrudOperation_1.Data;
using Microsoft.AspNetCore.Mvc;

namespace CrudOperation_1.Controllers
{
    public class NotificationController : Controller
    {
        private readonly DB _context;
        public NotificationController(DB _contex)
        {
            _context = _contex;
        }
        public IActionResult MarkAllRead()
        {
            var unread = _context.Notifications
                                 .Where(n => !n.IsRead)
                                 .ToList();

            unread.ForEach(n => n.IsRead = true);
            _context.SaveChanges();
            return Ok();
        }
    }
}
