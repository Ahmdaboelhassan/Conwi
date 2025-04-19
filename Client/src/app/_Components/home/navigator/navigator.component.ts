import {
  Component,
  ElementRef,
  HostListener,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ContactService } from 'src/app/_services/contact.service';
import { IconService } from 'src/app/_services/Icons.service';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.scss'],
  standalone: true,
  imports: [FontAwesomeModule, RouterLink, RouterLinkActive],
})
export class NavigatorComponent implements OnInit {
  icons;
  unReadNotifiaction = signal(0);
  unReadMessage = signal(0);
  lastScrollTop: number = 0;

  @ViewChild('navigator', { static: true }) navigator: ElementRef;

  constructor(
    private iconService: IconService,
    private notificationService: NotificationService,
    private contactService: ContactService
  ) {
    this.icons = this.iconService.icons;
  }

  ngOnInit(): void {
    this.notificationService.GetUnReadNotification().subscribe((count) => {
      this.unReadNotifiaction.set(count);
    });

    this.contactService.GetUnReadMessages().subscribe({
      next: (count) => this.unReadMessage.set(count),
    });
  }

  @HostListener('window:scroll', ['$event'])
  hideOnscroll() {
    const currentScrollTop = window.scrollY;
    const elementDisplay: string =
      currentScrollTop > this.lastScrollTop ? 'none' : 'flex';

    this.navigator.nativeElement.style.display = elementDisplay;

    this.lastScrollTop = currentScrollTop <= 0 ? 0 : currentScrollTop;
  }
}
