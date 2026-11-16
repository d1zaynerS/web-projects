using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using PizzaStar.Data;
using PizzaStar.Models;

namespace PizzaStar.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public static HashSet<string> OnlineUsers = new HashSet<string>();

        public ChatHub(ApplicationContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                OnlineUsers.Add(userName);
                await Clients.All.SendAsync("UpdateOnlineUsers", OnlineUsers);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userName = Context.User.Identity.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                OnlineUsers.Remove(userName);
                await Clients.All.SendAsync("UpdateOnlineUsers", OnlineUsers);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string messageText)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user != null)
            {
                var message = new Message
                {
                    SenderId = user.Id,
                    Content = messageText,
                    SentAt = DateTime.Now
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                await Clients.All.SendAsync("ReceiveMessage", user.UserName, messageText, message.SentAt.ToString("HH:mm"), message.Id);
            }
        }

        public async Task SendPrivateMessage(string receiverId, string messageText)
        {
            var sender = await _userManager.GetUserAsync(Context.User);
            if (sender != null)
            {
                var message = new Message
                {
                    SenderId = sender.Id,
                    ReceiverId = receiverId,
                    Content = messageText,
                    SentAt = DateTime.Now
                };
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                await Clients.User(receiverId).SendAsync("ReceivePrivateMessage", sender.UserName, messageText, message.Id);
                await Clients.Caller.SendAsync("ReceivePrivateMessage", sender.UserName, messageText, message.Id);
            }
        }

        public async Task DeleteMessage(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {

                var user = await _userManager.GetUserAsync(Context.User);
                if (message.SenderId == user.Id || Context.User.IsInRole("Admin"))
                {
                    _context.Messages.Remove(message);
                    await _context.SaveChangesAsync();

                    await Clients.All.SendAsync("MessageDeleted", messageId);
                }
            }
        }
    }
}