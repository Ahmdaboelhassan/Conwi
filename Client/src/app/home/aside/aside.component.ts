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
    private iconService: IconService,
    private authService: AuthService
  ) {
    this.icons = this.iconService.icons;
  }
  logout() {
    this.authService.logout();
  }
}
