using ChatApi.Controllers;
using ChatApi.Models;
using ChatApi.Services;
using ChatApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace ChatApi.Tests
{
    public class ChatControllerTests
    {
        [Fact]
        public void GetMessages_ReturnsOkResultWithMessages()
        {
            // Arrange
            var service = new ChatMessageService();
            var mockHubContext = new Mock<IHubContext<ChatHub>>();

            service.AddMessage(new ChatMessage
            {
                User = "Felipe",
                Message = "Hola desde el controlador"
            });

            var controller = new ChatController(service, mockHubContext.Object);

            // Act
            var result = controller.GetMessages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var messages = Assert.IsAssignableFrom<IEnumerable<ChatMessage>>(okResult.Value);

            Assert.Single(messages);
        }
    }
}
