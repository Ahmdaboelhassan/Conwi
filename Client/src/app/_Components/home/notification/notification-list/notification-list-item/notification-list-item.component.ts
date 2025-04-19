import { NgClass } from '@angular/common';
import { Component, inject, Input } from '@angular/core';
import { MomentModule } from 'ngx-moment';
import { Notification } from 'src/app/_interface/Response/Notifcation';
import { LoaderService } from 'src/app/_services/loader.service';
import { NotificationService } from 'src/app/_services/notification.service';

@Component({
  selector: 'app-notification-list-item',
  standalone: true,
  imports: [MomentModule, NgClass],
  templateUrl: './notification-list-item.component.html',
  styleUrl: './notification-list-item.component.scss',
})
export class NotificationListItemComponent {
  @Input() notificationItem: Notification;

  notificationService = inject(NotificationService);
  loaderService = inject(LoaderService);
  MarkRead(id: number) {
    this.notificationItem.isRead = true;
    this.loaderService.isDisabled = true;
    this.notificationService.MarkAsRead(id).subscribe({});
    this.loaderService.isDisabled = false;
  }
}
