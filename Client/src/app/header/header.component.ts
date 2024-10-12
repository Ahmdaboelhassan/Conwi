import { Component, OnInit } from '@angular/core';
import { AuthService } from '../Services/auth.service';
import { FormsModule, NgForm } from '@angular/forms';

import { LoginModel } from '../Interfaces/Request/LoginModel';
import { User } from 'src/app/Models/User';
import { CommonModule } from '@angular/common';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IconService } from '../Services/Icons.service';

@Component({
  standalone: true,
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  imports: [FormsModule, FontAwesomeModule],
})
export class HeaderComponent implements OnInit {
  isAuth: boolean;
  icons;
  constructor(
    private authService: AuthService,
    private iconService: IconService
  ) {}

  ngOnInit(): void {
    this.icons = this.iconService.icons;
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
