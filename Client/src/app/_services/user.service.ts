import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { UserProfile } from '../_interface/Response/UserProfile';
import { UserCard } from '../_interface/Response/UserCard';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  GetUserProfile(id, userId): Observable<UserProfile> {
    const url = environment.baseUrl + `User/UserProfile/${id}/${userId}`;
    return this.http.get<UserProfile>(url);
  }

  UploadProfilePhoto(form) {
    const url = environment.baseUrl + `User/UploadProfilePhoto`;
    return this.http.post<boolean>(url, form);
  }

  SearchUsers(criteria = null, userId) {
    let url =
      environment.baseUrl +
      `User/SearchUsers?criteria=${criteria}&userId=${userId}`;
    return this.http.get<UserCard[]>(url);
  }

  ExploreUsers(userId) {
    let url = environment.baseUrl + `User/ExploreUsers/${userId}`;
    return this.http.get<UserCard[]>(url);
  }

  followUser(sourceId, destId) {
    const url = environment.baseUrl + `User/FollowUser`;
    return this.http.post(url, {
      sourceId: sourceId,
      destId: destId,
    });
  }
}
