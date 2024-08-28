import { Component, OnInit } from '@angular/core';
import { AuthService } from './Services/auth.service';
import { User } from 'Models/User';
import { take } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isAuth = false;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (user: User) => {
        this.isAuth = !!user;
      },
    });
  }
}
