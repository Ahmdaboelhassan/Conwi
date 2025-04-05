import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { PrivateChat } from '../_interface/Response/PrivateChat';
import { Chat } from '../_interface/Response/ChatItem';
import { Message } from '../_interface/Response/Message';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  chats = signal<Chat[]>([]);
  private privateChat: PrivateChat;

  constructor(private http: HttpClient) {}

  GetPrivateChat(userId, contactId) {
    const url =
      environment.baseUrl + `Message/GetPrivateChat/${userId}/${contactId}`;
    return this.http
      .get<PrivateChat>(url)
      .pipe(tap((chat) => (this.privateChat = chat)));
  }
  GetUsersChats(userId) {
    const url = environment.baseUrl + `Message/GetAllChats/${userId}`;
    return this.http
      .get<Chat[]>(url)
      .pipe(tap((chats) => this.chats.set(chats)));
  }
  SendMessage(message: Message) {
    const url = environment.baseUrl + `Message/SendMessage`;
    return this.http.post(url, message);
  }

  UpdateChatListWithLastMessage(msg: Message, receiverId: string) {
    this.chats.update((currentChats) => {
      const userChat = currentChats.filter((ch) => ch.userId == receiverId);
      let updatedChats: Chat[] = [];
      if (userChat.length > 0) {
        updatedChats = currentChats.map((chat) =>
          chat.userId === receiverId
            ? {
                ...chat,
                lastMessage: msg.content,
                lastMessageTime: msg.sendTime,
              }
            : chat
        );
      } else {
        const newChat: Chat = {
          lastMessage: msg.content,
          lastMessageTime: msg.sendTime,
          userFirstName: this.privateChat.firstName,
          userLastName: this.privateChat.lastName,
          userId: receiverId,
          userName: this.privateChat.username,
          userPhoto: this.privateChat.userPhoto,
        };
        currentChats.push(newChat);

        updatedChats = currentChats;
      }

      return updatedChats.sort((a, b) => {
        return (
          new Date(b.lastMessageTime).getTime() -
          new Date(a.lastMessageTime).getTime()
        );
      });
    });
  }
}
