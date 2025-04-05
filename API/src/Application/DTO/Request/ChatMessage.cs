namespace Application.DTO.Request; 

public record ChatMessage
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string SenderId { get; set; }
    public string RevieverId { get; set; }
    public bool IsReaded { get; set; }
    public bool IsDelivered { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime SendTime { get; set; }
    public DateTime DeliverTime { get; set; }

}

