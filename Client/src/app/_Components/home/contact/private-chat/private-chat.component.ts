import {
  AfterContentInit,
  AfterRenderRef,
  AfterViewInit,
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  EventEmitter,
  inject,
  OnDestroy,
  OnInit,
  Output,
  signal,
  ViewChild,
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
import * as signalR from '@microsoft/signalr';
import { MessageHub } from 'src/app/_hubs/message.hub';
import { finalize, Subscription, take } from 'rxjs';
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
export class PrivateChatComponent implements OnInit, AfterViewInit, OnDestroy {
  @Output() refreshChats = new EventEmitter<boolean>();
  @ViewChild('messagesList') messageContainer: ElementRef;
  private observer: MutationObserver;

  contactId = signal('');
  contactName = signal('User');
  contactPhoto = signal(environment.defualtProfilePhoto);
  isTyping = signal(false);
  messages = signal<Message[]>([]);
  IsLoading = signal(false);
  private messageSub: Subscription;
  private routeSub: Subscription;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private contactService: ContactService,
    private loadingService: LoaderService,
    private messageHub: MessageHub
  ) {}

  ngOnInit(): void {
    const currentUser = this.authService.getCurrentUserId();
    this.loadPrivateChat(currentUser);
    this.messageHub.listenOnMessageSent();
    this.listenOnLoader();
    this.listenOnTyping();
  }

  ngAfterViewInit(): void {
    this.observer = new MutationObserver(() => {
      this.scrollToBottom();
    });

    this.observer.observe(this.messageContainer.nativeElement, {
      childList: true,
      subtree: true,
    });
  }

  ngOnDestroy(): void {
    if (this.routeSub) {
      this.routeSub.unsubscribe();
    }

    if (this.messageSub) {
      this.messageSub.unsubscribe();
    }
  }

  loadPrivateChat(currentUser) {
    this.route.paramMap.subscribe({
      next: (params) => {
        this.routeSub = this.contactService
          .GetPrivateChat(currentUser, params.get('id'))
          .subscribe({
            next: (chat: PrivateChat) => {
              this.contactId.set(chat.userId);
              this.contactName.set(chat.username);
              this.contactPhoto.set(chat.userPhoto);
              this.messages.set(chat.messages);

              // Real Time
              this.listenOnMessageSent(currentUser, chat.userId);
            },
            error: (err) => console.log(err),
          });
      },
    });
  }

  listenOnMessageSent(currentUser, chatContact) {
    if (this.messageSub) {
      this.messageSub.unsubscribe();
    }

    this.messageSub = this.messageHub.messageSubject.subscribe({
      next: (msg) => {
        if (
          (msg.senderId === currentUser && msg.revieverId === chatContact) ||
          (msg.senderId === chatContact && msg.revieverId === currentUser)
        ) {
          this.messages.update((x) => [...x, msg]);
        }
      },
    });
  }

  listenOnLoader() {
    this.loadingService.isLoading$.subscribe({
      next: (isLoad) => this.IsLoading.set(isLoad),
    });
  }

  listenOnTyping() {
    this.messageHub.listenOnTyping();
    this.messageHub.isTypingEvent.subscribe({
      next: (senderId) => {
        if (senderId === this.contactId()) {
          this.isTyping.set(true);
          setTimeout(() => {
            this.isTyping.set(false);
          }, 500);
        }
      },
    });
  }

  scrollToBottom(): void {
    this.messageContainer.nativeElement.scrollTop =
      this.messageContainer.nativeElement.scrollHeight;
  }
}
