namespace Application.DTO.Request
{
    public record class ConfirmationRequest
    {
        public string Email { get; set; }
        public string ConfirmationUrl { get; set; }
    }
}