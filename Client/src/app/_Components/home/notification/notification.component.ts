import { Component, OnInit } from '@angular/core';
import { Notification } from 'src/app/_interface/Response/Notifcation';
import { AuthService } from 'src/app/_services/auth.service';
import { NotificationService } from 'src/app/_services/notification.service';
import { LoaderComponent } from '../../loader/loader.component';
import { LoaderService } from 'src/app/_services/loader.service';
import { NgClass } from '@angular/common';
import { NotificationListComponent } from './notification-list/notification-list.component';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [LoaderComponent, NgClass, NotificationListComponent],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss',
})
export class NotificationComponent implements OnInit {
  notificaions: Notification[] = [];
  isloading = false;

  constructor(
    private authService: AuthService,
    private notificationService: NotificationService,
    private loaderServcie: LoaderService
  ) {}

  ngOnInit(): void {
    this.GetUserNotfication();
    this.SubscribeToLoadEvent();
  }

  GetUserNotfication() {
    const userId = this.authService.getCurrentUserId();

    this.notificationService.GetUserNotifications(userId).subscribe({
      next: (userNotificaions) => (this.notificaions = userNotificaions),
    });
  }

  SubscribeToLoadEvent() {
    this.loaderServcie.isLoading$.subscribe({
      next: (isLoad) => {
        this.isloading = isLoad;
      },
    });
  }
}
