using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace API.Hubs;

public class MessageHub : Hub
{
    
    private static readonly ConcurrentDictionary<string, string> _userConnections = new();
    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()?.Request.Query["user"];

        if (!string.IsNullOrEmpty(userId))
        {
            // Update or add user connection
            _userConnections[userId] = Context.ConnectionId;
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        // Remove user connection on disconnect
        var userId = Context.GetHttpContext()?.Request.Query["userId"];

        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections.TryRemove(userId, out _);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendPrivateMessage(string senderId, string receiverId, string content)
    {
        // Send RealTime To Connections
        _userConnections.TryGetValue(senderId, out var senderConnectionId);
        _userConnections.TryGetValue(receiverId, out var receiverConnectionId);

        // Send message to both sender and receiver if they are connected
        if (!string.IsNullOrEmpty(receiverConnectionId))
        {
            await Clients.Client(receiverConnectionId).SendAsync("ReceivePrivateMessage", senderId , receiverId ,content);
        }

        if (!string.IsNullOrEmpty(senderConnectionId))
        {
            await Clients.Client(senderConnectionId).SendAsync("ReceivePrivateMessage", senderId , receiverId ,content);
        }
    }

    public async Task TriggerTyping(string senderId , string receiverId)
        {
        // Send RealTime To Connections
        _userConnections.TryGetValue(senderId, out var senderConnectionId);
        _userConnections.TryGetValue(receiverId, out var receiverConnectionId);

        // Send message to both sender and receiver if they are connected
        if (!string.IsNullOrEmpty(senderConnectionId) && !string.IsNullOrEmpty(receiverConnectionId))
        {
            await Clients.Client(receiverConnectionId).SendAsync("IsTypingMessage" , senderId);
        }
    }
}
    