import { Component } from '@angular/core';
import { IconService } from 'src/app/Services/Icons.service';
import { AuthService } from 'src/app/Services/auth.service';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.scss'],
})
export class AsideComponent {
  icons;
  constructor(
    private iconSservice: IconService,
    private authService: AuthService
  ) {
    this.icons = this.iconSservice.icons;
  }

  logout() {
    this.authService.logout();
  }
}
