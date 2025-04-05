using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class Message
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

    [ForeignKey(nameof(SenderId))]
    public AppUser Sender { get; set; }

    [ForeignKey(nameof(RevieverId))]
    public AppUser Reviever { get; set; }
}
