import { Component, inject, OnInit, signal } from '@angular/core';
import { PostComponent } from '../../post/post.component';
import { ReadPost } from 'src/app/Interfaces/Response/ReadPost';
import { UserService } from 'src/app/Services/user.service';
import { AuthService } from 'src/app/Services/auth.service';
import { User } from 'src/app/Models/User';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  styleUrls: ['./timeline.component.scss'],
  standalone: true,
  imports: [PostComponent],
})
export class TimelineComponent implements OnInit {
  posts = signal<ReadPost[]>([]);
  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {}
  ngOnInit(): void {
    this.authService.user$.subscribe({
      next: (user: User) => {
        this.userService.GetFollowingPosts(user.Id).subscribe({
          next: (res: ReadPost[]) => this.posts.set(res),
          error: (err) => console.log(err),
        });
      },
    });
  }
}
