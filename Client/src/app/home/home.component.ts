import { Component, inject, OnInit } from '@angular/core';
import { ProfileComponent } from './profile/profile.component';
import { TimelineComponent } from './timeline/timeline.component';
import { NavigatorComponent } from './navigator/navigator.component';
import { RouterOutlet } from '@angular/router';
import { LoaderService } from '../Services/loader.service';
import { LoaderComponent } from '../loader/loader.component';

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
    LoaderComponent,
  ],
})
export class HomeComponent implements OnInit {
  isloading: boolean = false;
  loaderService = inject(LoaderService);

  ngOnInit(): void {
    this.loaderService.isLoading$.subscribe({
      next: (isloadingResult: boolean) => (this.isloading = isloadingResult),
    });
  }
}
