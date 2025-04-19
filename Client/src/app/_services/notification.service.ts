import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Notification } from '../_interface/Response/Notifcation';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private http: HttpClient) {}

  GetUserNotifications(userId) {
    const url =
      environment.baseUrl + `Notification/GetUserNotifications/${userId}`;
    return this.http.get<Notification[]>(url);
  }

  GetUnReadNotification() {
    const url = environment.baseUrl + `Notification/GetUnReadNotification`;
    return this.http.get<number>(url);
  }

  MarkAsRead(id) {
    const url = environment.baseUrl + `Notification/ReadNotification/${id}`;
    return this.http.put<number>(url, {});
  }
}
