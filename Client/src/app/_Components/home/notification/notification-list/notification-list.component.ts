import { Component, Input } from '@angular/core';
import { NotificationListItemComponent } from './notification-list-item/notification-list-item.component';

@Component({
  selector: 'app-notification-list',
  standalone: true,
  imports: [NotificationListItemComponent],
  templateUrl: './notification-list.component.html',
  styleUrl: './notification-list.component.scss',
})
export class NotificationListComponent {
  @Input() notifiationList = [];
}
