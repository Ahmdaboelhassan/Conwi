import { Component } from '@angular/core';
import { IconService } from 'src/app/Services/Icons.service';

@Component({
  selector: 'app-navigator',
  templateUrl: './navigator.component.html',
  styleUrls: ['./navigator.component.scss'],
})
export class NavigatorComponent {
  icons;
  constructor(private iconService: IconService) {
    this.icons = this.iconService.icons;
  }
}
