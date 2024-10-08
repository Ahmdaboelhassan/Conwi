import { Component, OnInit } from '@angular/core';
import { AuthService } from '../Services/auth.service';
import { FormsModule, NgForm } from '@angular/forms';

import { LoginModel } from '../Interfaces/LoginModel';
import { User } from 'src/app/Models/User';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports: [FormsModule],
})
export class HeaderComponent implements OnInit {
  isAuth: boolean;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (user: User) => (this.isAuth = !!user),
    });

    this.authService.autoLogin();
  }

  Login(form: NgForm) {
    if (!form.valid) return;

    let model: LoginModel = {
      Email: form.value.username,
      Password: form.value.password,
    };
    this.authService.login(model);
  }

  logout() {
    this.authService.logout();
  }
}
