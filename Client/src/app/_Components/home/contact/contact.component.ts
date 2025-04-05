import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ChatListComponent } from './chat-list/chat-list.component';
import { PrivateChatComponent } from './private-chat/private-chat.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-contact',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ChatListComponent, RouterOutlet],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent {}
