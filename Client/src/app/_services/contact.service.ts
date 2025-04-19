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

  GetUnReadMessages() {
    const url = environment.baseUrl + `Message/GetUnReadMessages`;
    return this.http.get<number>(url);
  }

  ReadMessage(msgId) {
    const url = environment.baseUrl + `Message/ReadMessage/${msgId}`;
    return this.http.put<number>(url, {});
  }

  UpdateChatListWithLastMessage(content, receiver, sender, currentUser) {
    this.chats.update((currentChats) => {
      debugger;
      const userChat = currentChats.filter((ch) => ch.userId == receiver);
      let updatedChats: Chat[] = [];
      if (userChat.length > 0) {
        updatedChats = currentChats.map((chat) =>
          chat.userId === receiver
            ? {
                ...chat,
                lastMessageRead: sender == currentUser,
                lastMessage: content,
                lastMessageTime: new Date(),
              }
            : chat
        );
      } else {
        const newChat: Chat = {
          lastMessage: content,
          lastMessageTime: new Date(),
          lastMessageRead: sender == currentUser,
          userFirstName: this.privateChat?.firstName ?? 'New Message',
          userLastName: this.privateChat?.lastName,
          userId: receiver,
          userName: this.privateChat?.username,
          userPhoto:
            this.privateChat?.userPhoto ?? environment.defualtProfilePhoto,
        };
        updatedChats = [...currentChats, newChat];
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
