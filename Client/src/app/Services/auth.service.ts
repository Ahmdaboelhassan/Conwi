import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterModel } from '../Interfaces/RegisterModel';
import { LoginModel } from '../Interfaces/LoginModel';
import { AuthResponseModel } from '../Interfaces/AuthResponseModel';
import { BehaviorSubject, Observable, Subject, Subscription } from 'rxjs';
import { User } from 'Models/User';
import { environment } from 'src/environments/environment.development';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userSubject: BehaviorSubject<User | null> =
    new BehaviorSubject<User | null>(null);

  user$: Observable<User> = this.userSubject.asObservable();

  baseUrl = environment.baseUrl;

  constructor(private http: HttpClient, private toester: ToastrService) {}

  register(model: RegisterModel): Subscription {
    const url = environment.baseUrl + 'Auth/Register';

    return this.http.post<AuthResponseModel>(url, model).subscribe({
      next: (res) => this.manageLogin(res),
      error: (resError) => this.manageError(resError),
    });
  }

  login(model: LoginModel): Subscription {
    const url = environment.baseUrl + 'Auth/Login';

    return this.http.post<AuthResponseModel>(url, model).subscribe({
      next: (res) => this.manageLogin(res),
      error: (resError) => this.manageError(resError),
    });
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

  refreshToken() {}
  confirmEmail() {}
  resetPassword() {}

  private manageLogin(response: AuthResponseModel) {
    let registerdUser: User = new User(
      response.userName,
      response.email,
      response.token,
      response.expireOn,
      response.isEmailConfrimed
    );
    localStorage.setItem('user', JSON.stringify(registerdUser));
    this.userSubject.next(registerdUser);
    this.toester.success(`Hi ${registerdUser.Username}`);
  }

  private manageError(resError) {
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
    errorList.forEach((el) => {
      this.toester.error(el);
    });
  }
}
