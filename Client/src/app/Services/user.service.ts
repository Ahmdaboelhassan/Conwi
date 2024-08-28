import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { UserProfile } from '../Interfaces/UserProfile';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = '';

  constructor(private http: HttpClient) {}

  GetUserProfile(email): Observable<UserProfile> {
    const url = environment.baseUrl + `User/${email}`;

    return this.http.get<UserProfile>(url);
  }
}
