import { Routes } from '@angular/router';
import { TimelineComponent } from './home/timeline/timeline.component';
import { ProfileComponent } from './home/profile/profile.component';

export const routes: Routes = [
  { path: 'timeline', component: TimelineComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '**', component: TimelineComponent, pathMatch: 'full' },
];
