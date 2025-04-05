using Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.DTO.Request;

public class SendMessage
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


    public static implicit operator Message(SendMessage DTO)
    {
        return new Message
        {
            Id = DTO.Id,
            Content = DTO.Content,
            SenderId = DTO.SenderId,
            RevieverId = DTO.RevieverId,
            IsDeleted = DTO.IsDeleted,
            IsReaded = DTO.IsReaded,
            IsDelivered = DTO.IsDelivered,
            SendTime = DTO.SendTime,
            DeliverTime = DTO.DeliverTime,
        };
    }
}
