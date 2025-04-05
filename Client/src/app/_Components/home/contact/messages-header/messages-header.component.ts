import { Component, Input, input } from '@angular/core';
import { Message } from 'src/app/_interface/Response/Message';

@Component({
  selector: 'app-messages-header',
  standalone: true,
  imports: [],
  templateUrl: './messages-header.component.html',
  styleUrl: './messages-header.component.scss',
})
export class MessagesHeaderComponent {
  @Input() username;
  @Input() photo;

  messages: Message[] = [];
}
