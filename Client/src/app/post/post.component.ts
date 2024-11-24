import { Component, input, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { ReadPost } from '../_interface/Response/ReadPost';
import { PostService } from '../_services/post.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [],
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss',
})
export class PostComponent {
  post = input<ReadPost>();
  defualtImg = environment.defualtProfilePhoto;
  userId: string;

  constructor(
    private postService: PostService,
    private toastrService: ToastrService,
    private authService: AuthService
  ) {
    this.userId = this.authService.getCurrentUserId();
  }

  DeletePost(postid: number, e) {
    this.postService.DeletePost(postid, this.userId).subscribe({
      next: () => {
        this.toastrService.success('Post Deleted Successfully');
        e.target.closest('#post').remove();
      },
    });
  }
}
