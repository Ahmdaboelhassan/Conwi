import { Component, OnInit } from '@angular/core';
import { AuthService } from './Services/auth.service';
import { User } from 'src/app/Models/User';
import { HeaderComponent } from './header/header.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './Register/Register.component';
import { NavigatorComponent } from './home/navigator/navigator.component';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { LoaderComponent } from './loader/loader.component';
import { LoaderService } from './Services/loader.service';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [
    HeaderComponent,
    HomeComponent,
    RegisterComponent,
    NavigatorComponent,
    RouterLink,
    RouterLinkActive,
  ],
})
export class AppComponent implements OnInit {
  isAuth: boolean = false;
  isloading: boolean = false;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (user: User) => (this.isAuth = !!user),
    });
  }
}
