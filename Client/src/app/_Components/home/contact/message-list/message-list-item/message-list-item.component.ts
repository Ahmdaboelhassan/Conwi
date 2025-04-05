import { DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Message } from 'src/app/_interface/Response/Message';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-message-list-item',
  standalone: true,
  imports: [DatePipe],
  templateUrl: './message-list-item.component.html',
  styleUrl: './message-list-item.component.scss',
})
export class MessageListItemComponent implements OnInit {
  @Input() message: Message;
  sentStyle: string = 'bg-black ms-auto';
  currentUserId: string = '';
  IsSent = false;
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.currentUserId = this.authService.getCurrentUserId();
    this.IsSent = this.currentUserId === this.message?.senderId;
  }
}
