import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ReadPost } from '../_interface/Response/ReadPost';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  constructor(private http: HttpClient) {}

  GetPosts(userId): Observable<ReadPost[]> {
    const url = environment.baseUrl + `Post/UserPosts/${userId}`;
    return this.http.get<ReadPost[]>(url);
  }

  GetFollowingPosts(userId) {
    const url = environment.baseUrl + `Post/FollowingPosts/${userId}`;
    return this.http.get<ReadPost[]>(url);
  }

  CreatePost(form) {
    const url = environment.baseUrl + `Post/CreatePost`;
    return this.http.post<boolean>(url, form);
  }
  DeletePost(postId, userId) {
    const url = environment.baseUrl + `Post/DeletePost/${postId}/${userId}`;
    return this.http.get<boolean>(url);
  }
}
