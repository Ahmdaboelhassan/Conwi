import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  inject,
  OnInit,
  Output,
  signal,
} from '@angular/core';
import { SendInputControlComponent } from '../send-input-control/send-input-control.component';
import { MessageListComponent } from '../message-list/message-list.component';
import { MessagesHeaderComponent } from '../messages-header/messages-header.component';
import { environment } from 'src/environments/environment';
import { Message } from 'src/app/_interface/Response/Message';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { ContactService } from 'src/app/_services/contact.service';
import { PrivateChat } from 'src/app/_interface/Response/PrivateChat';
import { LoaderService } from 'src/app/_services/loader.service';
import { LoaderComponent } from 'src/app/_Components/loader/loader.component';

@Component({
  selector: 'app-private-chat',
  standalone: true,
  imports: [
    SendInputControlComponent,
    MessageListComponent,
    MessagesHeaderComponent,
    LoaderComponent,
  ],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './private-chat.component.html',
  styleUrl: './private-chat.component.scss',
})
export class PrivateChatComponent implements OnInit {
  contactId = signal('');
  contactName = signal('User');
  contactPhoto = signal(environment.defualtProfilePhoto);
  @Output() refreshChats = new EventEmitter<boolean>();

  messages = signal<Message[]>([]);
  IsLoading = signal(false);
  private route = inject(ActivatedRoute);
  private authService = inject(AuthService);
  private contactService = inject(ContactService);
  private loadingService = inject(LoaderService);

  ngOnInit(): void {
    this.loadingService.isLoading$.subscribe({
      next: (isLoad) => this.IsLoading.set(isLoad),
    });

    const currentUser = this.authService.getCurrentUserId();
    this.route.params.subscribe({
      next: (params) => {
        this.contactService
          .GetPrivateChat(currentUser, params['id'])
          .subscribe({
            next: (chat: PrivateChat) => {
              this.contactId.set(chat.userId);
              this.contactName.set(chat.username);
              this.contactPhoto.set(chat.userPhoto);
              this.messages.set(chat.messages);
            },
            error: (err) => console.log(err),
          });
      },
    });
  }

  messageSent(msg: Message) {
    this.messages.update((x) => [...x, msg]);
    this.refreshChats.emit(true);
  }
}
