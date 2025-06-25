namespace ChatApi.Models
{
    public class ChatMessageRequest
    {
        public string User { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
