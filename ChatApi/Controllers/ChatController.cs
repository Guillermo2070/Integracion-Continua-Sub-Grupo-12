using ChatApi.Models;
using ChatApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ChatApi.Hubs;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly ChatMessageService _service;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(ChatMessageService service, IHubContext<ChatHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
        }

        [HttpGet("messages")]
        public ActionResult<IEnumerable<ChatMessage>> GetMessages()
        {
            return Ok(_service.GetAllMessages());
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageRequest request)
        {
            var message = new ChatMessage
            {
                User = request.User,
                Message = request.Message,
                Timestamp = DateTime.Now
            };

            _service.AddMessage(message);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Message);

            return Ok(message);
        }
    }
}
