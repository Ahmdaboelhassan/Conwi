namespace Application.DTO.Response
{
    public record UserCard
    {
        public string userId { get; set; }        
        public string firstName { get; set; }        
        public string lastName { get; set; }        
        public string UserName { get; set; }        
        public string photo { get; set; }
        public bool following { get; set; }
    }
}