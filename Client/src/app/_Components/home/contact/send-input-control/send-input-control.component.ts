import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Message } from 'src/app/_interface/Response/Message';
import { AuthService } from 'src/app/_services/auth.service';
import { ContactService } from 'src/app/_services/contact.service';
import { LoaderService } from 'src/app/_services/loader.service';
import { MessageHub } from 'src/app/_hubs/message.hub';

@Component({
  selector: 'app-send-input-control',
  standalone: true,
  imports: [],
  templateUrl: './send-input-control.component.html',
  styleUrl: './send-input-control.component.scss',
})
export class SendInputControlComponent {
  private currentUserId = '';
  @Input() revieverId;

  constructor(
    private contactService: ContactService,
    private authService: AuthService,
    private loaderService: LoaderService,
    private messageHub: MessageHub
  ) {
    this.currentUserId = this.authService.getCurrentUserId();
  }

  SendMessage(content: HTMLInputElement) {
    this.loaderService.isDisabled = true;

    const msg: Message = {
      content: content.value,
      sendTime: new Date(),
      senderId: this.currentUserId,
      revieverId: this.revieverId,
    };

    this.contactService.SendMessage(msg).subscribe({
      next: () => {
        this.messageHub.sendMessageSignalR(msg);
      },
    });

    this.loaderService.isDisabled = false;
    content.value = '';
  }

  TriggerIsTypeEvent() {
    this.messageHub.tiggerTyping(this.currentUserId, this.revieverId);
  }
}
