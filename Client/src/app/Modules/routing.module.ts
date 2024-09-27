import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileComponent } from '../home/profile/profile.component';
import { TimelineComponent } from '../home/timeline/timeline.component';

const routes: Routes = [
  { path: 'timeline', component: TimelineComponent },
  { path: 'profile', component: ProfileComponent },
  { path: '**', component: TimelineComponent, pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class RoutingModule {}
