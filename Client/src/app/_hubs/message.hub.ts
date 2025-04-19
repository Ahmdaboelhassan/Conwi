import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message } from '../_interface/Response/Message';

@Injectable({
  providedIn: 'root',
})
export class MessageHub {
  messageSubject = new Subject<Message>();
  isTypingEvent = new Subject<string>();

  private hubConnection: signalR.HubConnection;

  StartConnention(user) {
    if (user) {
      this.hubConnection = new signalR.HubConnectionBuilder()
        .withUrl(`${environment.baseUrl}MessageHub?user=${user}`)
        .build();
      this.hubConnection.start().catch((err) => console.error(err));
    } else {
      console.error('Sender Or Receiver Are Not Sent');
    }
  }

  listenOnMessageSent() {
    this.hubConnection.off('ReceivePrivateMessage');

    this.hubConnection.on(
      'ReceivePrivateMessage',
      (sender, receiver, content) => {
        var message: Message = {
          senderId: sender,
          revieverId: receiver,
          content: content,
          sendTime: new Date(),
        };
        this.messageSubject.next(message);
      }
    );
  }

  listenOnTyping() {
    this.hubConnection.on('IsTypingMessage', (senderId) => {
      this.isTypingEvent.next(senderId);
    });
  }

  sendMessageSignalR(msg: Message) {
    this.hubConnection
      .invoke('SendPrivateMessage', msg.senderId, msg.revieverId, msg.content)
      .catch((err) => console.error(err));
  }

  tiggerTyping(senderId, revieverId) {
    this.hubConnection
      .invoke('TriggerTyping', senderId, revieverId)
      .catch((err) => console.error(err));
  }
}
