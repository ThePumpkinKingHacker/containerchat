using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            var sanitizer = new HtmlSanitizer();
            var sanitized = sanitizer.Sanitize(message);

            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, sanitized);
        }
    }
}