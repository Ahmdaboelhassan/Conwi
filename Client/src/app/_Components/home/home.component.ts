import { Component } from '@angular/core';
import { NavigatorComponent } from './navigator/navigator.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [NavigatorComponent, RouterOutlet],
})
export class HomeComponent {}
