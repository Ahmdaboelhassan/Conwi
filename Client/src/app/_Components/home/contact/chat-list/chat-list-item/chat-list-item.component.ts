import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DateFnsModule } from 'ngx-date-fns';
import { MomentModule } from 'ngx-moment';
import { Chat } from 'src/app/_interface/Response/ChatItem';

@Component({
  selector: 'app-chat-list-item',
  standalone: true,
  imports: [RouterLink, MomentModule, NgClass],
  templateUrl: './chat-list-item.component.html',
  styleUrl: './chat-list-item.component.scss',
})
export class ChatListItemComponent {
  @Input() chat: Chat;
}
