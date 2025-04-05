import { Component, Input } from '@angular/core';
import { MessageListItemComponent } from './message-list-item/message-list-item.component';
import { Message } from 'src/app/_interface/Response/Message';

@Component({
  selector: 'app-message-list',
  standalone: true,
  imports: [MessageListItemComponent],
  templateUrl: './message-list.component.html',
  styleUrl: './message-list.component.scss',
})
export class MessageListComponent {
  @Input() messages: Message[] = [];
}
