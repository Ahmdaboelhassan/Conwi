using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class AuthResponse
    {
        public IList<string>? Messages { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireOn { get; set; }
        public bool IsAuthenticated  { get; set; }
        public bool IsEmailConfrimed  { get; set; }
    }
}