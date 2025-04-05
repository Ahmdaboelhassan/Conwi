import { Component, input } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { ReadPost } from '../../_interface/Response/ReadPost';
import { PostService } from '../../_services/post.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../_services/auth.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [NgClass],
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

  LikePost(postid: number, button: HTMLElement) {
    debugger;
    const primaryLoading = document.getElementById('loader');
    const loader = button.querySelector('.loader');
    const text = button.querySelector('.text');

    text.classList.add('hidden');
    loader.classList.remove('hidden');
    primaryLoading.classList.add('opacity-0');

    this.postService.LikePost(postid, this.userId).subscribe({
      next: () => {
        this.toastrService.success('Post Liked Successfully');
        primaryLoading.classList.remove('opacity-0');
        loader.classList.add('hidden');
        text.classList.remove('hidden');

        if (this.post().isLiked) {
          this.post().likes--;
          this.post().isLiked = false;
        } else {
          this.post().likes++;
          this.post().isLiked = true;
        }
      },
    });
  }
}
