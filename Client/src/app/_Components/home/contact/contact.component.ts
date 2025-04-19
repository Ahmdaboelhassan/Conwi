import {
  ChangeDetectionStrategy,
  Component,
  HostListener,
  OnInit,
  signal,
} from '@angular/core';
import { ChatListComponent } from './chat-list/chat-list.component';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-contact',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [ChatListComponent, RouterOutlet, NgClass],
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.scss',
})
export class ContactComponent {
  isMobile = false;
  hideChats = signal(false);
  currentChildRoute = '';

  constructor(private router: Router, private route: ActivatedRoute) {
    this.updateMobileState();
    router.events.subscribe(() => {
      this.currentChildRoute = this.router.url;
      this.hideChats.set(
        this.currentChildRoute.includes('/chat/') && this.isMobile
      );
    });
  }

  @HostListener('window:resize')
  updateMobileState() {
    this.isMobile = window.innerWidth < 1024;
  }
}
