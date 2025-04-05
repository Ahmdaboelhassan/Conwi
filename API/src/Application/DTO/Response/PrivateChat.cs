using Application.DTO.Request;

namespace Application.DTO.Response
{
    public record PrivateChat
    {
        public string username{ get; set; }
        public string userId{ get; set; }
        public string userPhoto { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public IEnumerable<ChatMessage> messages { get; set; }
    }
}
