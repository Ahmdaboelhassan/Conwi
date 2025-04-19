import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './_Components/header/header.component';
import { HomeComponent } from './_Components/home/home.component';
import { RegisterComponent } from './_Components/Register/Register.component';
import { AuthService } from './_services/auth.service';
import { User } from './_models/User';
import { MessageHub } from './_hubs/message.hub';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [HeaderComponent, HomeComponent, RegisterComponent],
})
export class AppComponent implements OnInit {
  isAuth: boolean = false;
  isloading: boolean = false;
  constructor(
    private authService: AuthService,
    private messageHub: MessageHub
  ) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (user: User) => {
        this.isAuth = !!user;
        if (user) {
          this.messageHub.StartConnention(user.Id);
        }
      },
    });
  }
}
