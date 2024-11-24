import { Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AuthService } from '../_services/auth.service';
import { IconService } from '../_services/Icons.service';
import { User } from '../_models/User';
import { LoginModel } from '../_interface/Request/LoginModel';

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
