import { Routes } from '@angular/router';
import { TimelineComponent } from './home/timeline/timeline.component';
import { ProfileComponent } from './home/profile/profile.component';
import { ExploreComponent } from './home/explore/explore.component';

export const routes: Routes = [
  { path: 'timeline', component: TimelineComponent },
  { path: 'profile', component: ProfileComponent },
  { path: 'explore', component: ExploreComponent },
  { path: '**', component: TimelineComponent, pathMatch: 'full' },
];
