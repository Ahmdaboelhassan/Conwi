import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ChatListItemComponent } from './chat-list-item/chat-list-item.component';
import { Chat } from 'src/app/_interface/Response/ChatItem';
import { ContactService } from 'src/app/_services/contact.service';
import { AuthService } from 'src/app/_services/auth.service';
import { MessageHub } from 'src/app/_hubs/message.hub';

@Component({
  selector: 'app-chat-list',
  standalone: true,
  imports: [ChatListItemComponent],
  templateUrl: './chat-list.component.html',
  styleUrl: './chat-list.component.scss',
})
export class ChatListComponent implements OnInit {
  chats = computed(() => this.contactService.chats());

  private contactService = inject(ContactService);
  private messageHub = inject(MessageHub);
  private authService = inject(AuthService);

  ngOnInit(): void {
    const currentUser = this.authService.getCurrentUserId();
    this.contactService.GetUsersChats(currentUser).subscribe();

    this.listenOnNewMessage(currentUser);
  }

  listenOnNewMessage(currentUser) {
    this.messageHub.listenOnMessageSent();
    this.messageHub.messageSubject.subscribe({
      next: (msg) => {
        debugger;
        const chatUser =
          msg.senderId == currentUser ? msg.revieverId : msg.senderId;
        this.contactService.UpdateChatListWithLastMessage(
          msg.content,
          chatUser,
          msg.senderId,
          currentUser
        );
      },
    });
  }
}
