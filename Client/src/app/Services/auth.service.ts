import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterModel } from '../Interfaces/RegisterModel';
import { LoginModel } from '../Interfaces/LoginModel';
import { AuthResponseModel } from '../Interfaces/AuthResponseModel';
import { BehaviorSubject, Observable, Subject, Subscription } from 'rxjs';
import { User } from 'Models/User';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userSubject: BehaviorSubject<User | null> =
    new BehaviorSubject<User | null>(null);
  private errorsSubject: Subject<string[]> = new Subject();

  user$: Observable<User> = this.userSubject.asObservable();
  errors$: Observable<string[]> = this.errorsSubject.asObservable();

  baseUrl: string = 'https://localhost:5000/api/Auth/';

  constructor(private http: HttpClient) {}

  register(model: RegisterModel): Subscription {
    const url = this.baseUrl + 'Register';

    return this.http.post<AuthResponseModel>(url, model).subscribe({
      next: (res) => this.manageLogin(res),
      error: (resError) => this.manageError(resError),
    });
  }

  login(model: LoginModel): Subscription {
    const url = this.baseUrl + 'Login';

    return this.http.post<AuthResponseModel>(url, model).subscribe({
      next: (res) => this.manageLogin(res),
      error: (resError) => this.manageError(resError),
    });
  }

  manageLogin(response: AuthResponseModel) {
    let registerdUser: User = new User(
      response.userName,
      response.email,
      response.token,
      response.expireOn,
      response.isEmailConfrimed
    );
    localStorage.setItem('user', JSON.stringify(registerdUser));
    console.log('ssss');
    this.userSubject.next(registerdUser);
  }

  autoLogin() {
    if (localStorage.getItem('user')) {
      let localStorageUser = JSON.parse(localStorage.getItem('user'));

      let singInUser = new User(
        localStorageUser.Username,
        localStorageUser.Email,
        localStorageUser._token,
        localStorageUser.ExpireOn,
        localStorageUser.IsEmailConfrimed
      );

      if (singInUser.getToken) {
        this.userSubject.next(singInUser);
      }
    }
  }
  logout(): void {
    this.userSubject.next(null);

    if (localStorage.getItem('user')) {
      localStorage.removeItem('user');
    }
  }

  manageError(resError) {
    const errorObject = resError.error;
    let errorList: string[] = [];
    if (errorObject.errors) {
      Object.keys(errorObject.errors).forEach((key) => {
        errorList.push(...errorObject.errors[key]);
      });
    } else if (errorObject) {
      errorList.push(...errorObject);
    } else {
      errorList.push('Unknown Error Occured');
    }
    this.errorsSubject.next(errorList);
  }

  confirmEmail() {}
  resetPassword() {}
}
