namespace Application.DTO.Request
{
    public record class ConfirmationResponse
    {
        public string? Token { get; set; }
        public string? Messages { get; set; }
        public bool isGenerated { get; set; }
    }
}