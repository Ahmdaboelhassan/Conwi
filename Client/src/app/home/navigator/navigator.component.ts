import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IconService } from 'src/app/Services/Icons.service';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.scss'],
  imports: [FontAwesomeModule, RouterLink, RouterLinkActive],
  standalone: true,
})
export class NavigatorComponent {
  icons;
  constructor(private iconService: IconService) {
    this.icons = this.iconService.icons;
  }
}
