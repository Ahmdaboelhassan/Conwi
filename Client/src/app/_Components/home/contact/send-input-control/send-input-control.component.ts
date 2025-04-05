import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { Message } from 'src/app/_interface/Response/Message';
import { AuthService } from 'src/app/_services/auth.service';
import { ContactService } from 'src/app/_services/contact.service';
import { LoaderService } from 'src/app/_services/loader.service';

@Component({
  selector: 'app-send-input-control',
  standalone: true,
  imports: [],
  templateUrl: './send-input-control.component.html',
  styleUrl: './send-input-control.component.scss',
})
export class SendInputControlComponent {
  private contactService = inject(ContactService);
  private authService = inject(AuthService);
  private loaderService = inject(LoaderService);
  @Output() messageSendEvent = new EventEmitter<Message>();
  @Input() revieverId;

  SendMessage(content: HTMLInputElement) {
    const currentUserId = this.authService.getCurrentUserId();
    const msg: Message = {
      content: content.value,
      sendTime: new Date(),
      senderId: currentUserId,
      revieverId: this.revieverId,
    };

    this.loaderService.isDisabled = true;
    this.messageSendEvent.emit(msg);
    this.contactService.UpdateChatListWithLastMessage(msg, this.revieverId);
    this.contactService.SendMessage(msg).subscribe();
    content.value = '';
    this.loaderService.isDisabled = false;
  }
}
