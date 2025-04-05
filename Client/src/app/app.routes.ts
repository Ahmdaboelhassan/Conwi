import { Routes } from '@angular/router';
import { TimelineComponent } from './_Components/home/timeline/timeline.component';
import { ProfileComponent } from './_Components/home/profile/profile.component';
import { ExploreComponent } from './_Components/home/explore/explore.component';
import { ContactComponent } from './_Components/home/contact/contact.component';
import { PrivateChatComponent } from './_Components/home/contact/private-chat/private-chat.component';

export const routes: Routes = [
  { path: 'timeline', component: TimelineComponent },
  {
    path: 'profile',
    children: [
      { path: '', component: ProfileComponent },
      { path: ':userId', component: ProfileComponent },
    ],
  },
  {
    path: 'explore',
    loadComponent: () => ExploreComponent,
  },
  {
    path: 'contact',
    loadComponent: () => ContactComponent,
    children: [{ path: ':id', loadComponent: () => PrivateChatComponent }],
  },
  { path: '**', component: TimelineComponent, pathMatch: 'full' },
];
