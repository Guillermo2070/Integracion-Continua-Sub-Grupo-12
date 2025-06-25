using ChatApi.Models;
using System.Collections.Generic;

namespace ChatApi.Services
{
    public class ChatMessageService
    {
        private readonly List<ChatMessage> _messages = new();

        public void AddMessage(ChatMessage message)
        {
            _messages.Add(message);
        }

        public IEnumerable<ChatMessage> GetAllMessages()
        {
            return _messages;
        }
    }
}
