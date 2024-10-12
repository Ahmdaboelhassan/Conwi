import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { UserProfile } from '../Interfaces/Response/UserProfile';
import { ReadPost } from '../Interfaces/Response/ReadPost';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  GetUserProfile(email): Observable<UserProfile> {
    const url = environment.baseUrl + `User/UserProfile/${email}`;
    return this.http.get<UserProfile>(url);
  }

  GetPosts(userId): Observable<ReadPost[]> {
    const url = environment.baseUrl + `User/UserPosts/${userId}`;
    return this.http.get<ReadPost[]>(url);
  }

  GetFollowingPosts(userId) {
    const url = environment.baseUrl + `User/FollowingPosts/${userId}`;
    return this.http.get<ReadPost[]>(url);
  }

  CreatePost(form) {
    const url = environment.baseUrl + `User/CreatePost`;
    return this.http.post<boolean>(url, form);
  }

  UploadProfilePhoto(form) {
    const url = environment.baseUrl + `User/UploadProfilePhoto`;
    return this.http.post<boolean>(url, form);
  }
}
