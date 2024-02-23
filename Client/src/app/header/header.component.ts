import { Component, OnInit } from '@angular/core';
import { AuthService } from '../Services/auth.service';
import { NgForm } from '@angular/forms';

import { LoginModel } from '../Interfaces/LoginModel';
import { User } from 'Models/User';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  isAuth: boolean = false;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.autoLogin();

    this.authService.user$.subscribe({
      next: (user: User) => (this.isAuth = !!user),
    });
  }

  Login(form: NgForm) {
    if (!form.valid) return;

    let model: LoginModel = {
      Email: form.value.username,
      Password: form.value.password,
    };
    this.authService.login(model);
  }
}
