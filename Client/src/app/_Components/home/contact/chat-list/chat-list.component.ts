import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ChatListItemComponent } from './chat-list-item/chat-list-item.component';
import { Chat } from 'src/app/_interface/Response/ChatItem';
import { ContactService } from 'src/app/_services/contact.service';
import { AuthService } from 'src/app/_services/auth.service';

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
  private authService = inject(AuthService);

  ngOnInit(): void {
    this.contactService
      .GetUsersChats(this.authService.getCurrentUserId())
      .subscribe();
  }
}
