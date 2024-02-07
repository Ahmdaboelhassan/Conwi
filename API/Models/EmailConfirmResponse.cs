namespace API.Models
{
    public class ConfirmationResponse
    {
        public string? Token { get; set; }
        public string? Messages { get; set; }
        public bool isGenerated  { get; set; }
    }
}