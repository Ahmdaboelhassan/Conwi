import { Component, OnInit, signal } from '@angular/core';
import { PostComponent } from '../../post/post.component';
import { ReadPost } from 'src/app/_interface/Response/ReadPost';
import { AuthService } from 'src/app/_services/auth.service';
import { LoaderService } from 'src/app/_services/loader.service';
import { User } from 'src/app/_models/User';
import { PostService } from 'src/app/_services/post.service';
import { NgClass } from '@angular/common';
import { LoaderComponent } from '../../loader/loader.component';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  styleUrls: ['./timeline.component.scss'],
  standalone: true,
  imports: [PostComponent, LoaderComponent, NgClass],
})
export class TimelineComponent implements OnInit {
  posts = signal<ReadPost[]>([]);
  isloading: boolean = false;

  constructor(
    private postService: PostService,
    private authService: AuthService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (user: User) => {
        this.postService.GetFollowingPosts(user.Id).subscribe({
          next: (res: ReadPost[]) => this.posts.set(res),
          error: (err) => console.log(err),
        });
      },
    });

    this.loaderService.isLoading$.subscribe({
      next: (isloadingResult: boolean) => (this.isloading = isloadingResult),
    });
  }
}
