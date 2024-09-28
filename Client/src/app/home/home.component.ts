import { Component } from '@angular/core';
import { ProfileComponent } from './profile/profile.component';
import { TimelineComponent } from './timeline/timeline.component';
import { NavigatorComponent } from './navigator/navigator.component';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [
    ProfileComponent,
    TimelineComponent,
    NavigatorComponent,
    RouterOutlet,
  ],
})
export class HomeComponent {}
