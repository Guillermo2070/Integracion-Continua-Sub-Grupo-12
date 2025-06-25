using ChatApi.Models;
using ChatApi.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApi.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatMessageService _service;

        public ChatHub(ChatMessageService service)
        {
            _service = service;
        }

        public async Task SendMessage(string user, string message)
        {
            var chatMessage = new ChatMessage
            {
                User = user,
                Message = message,
                Timestamp = DateTime.Now
            };

            _service.AddMessage(chatMessage);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
