using ChatApi.Services;
using ChatApi.Models;
using Xunit;

namespace ChatApi.Tests
{
    public class ChatMessageServiceTests
    {
        [Fact]
        public void ShouldAddAndReturnMessages()
        {
            var service = new ChatMessageService();
            service.AddMessage(new ChatMessage
            {
                User = "Felipe",
                Message = "Hola desde la prueba"
            });

            var result = service.GetAllMessages().ToList();

            Assert.Single(result);
            Assert.Equal("Felipe", result[0].User);
            Assert.Equal("Hola desde la prueba", result[0].Message);
        }
    }
}
