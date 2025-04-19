import { Component, ElementRef, input, ViewChild } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { ReadPost } from '../../_interface/Response/ReadPost';
import { PostService } from '../../_services/post.service';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../_services/auth.service';
import { NgClass } from '@angular/common';
import { LoaderService } from 'src/app/_services/loader.service';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [NgClass],
  templateUrl: './post.component.html',
  styleUrl: './post.component.scss',
})
export class PostComponent {
  post = input<ReadPost>();
  likedButtonHoverd = false;
  defualtImg = environment.defualtProfilePhoto;
  @ViewChild('postElement') postElement: ElementRef;
  userId: string;

  constructor(
    private postService: PostService,
    private toastrService: ToastrService,
    private authService: AuthService,
    private loaderService: LoaderService
  ) {
    this.userId = this.authService.getCurrentUserId();
  }

  DeletePost(postid: number, e) {
    this.loaderService.isDisabled = true;
    this.postElement.nativeElement.classList.add('opacity-50');
    this.postService.DeletePost(postid, this.userId).subscribe({
      next: () => {
        this.postElement.nativeElement.remove();
        this.toastrService.success('Post Deleted Successfully');
      },
      error: () => {
        this.postElement.nativeElement.classList.remove('opacity-50');
        this.toastrService.error('Error Happend');
      },
      complete: () => {
        this.loaderService.isDisabled = false;
      },
    });
  }

  LikePost(postid: number, button: HTMLElement) {
    if (this.post().isLiked) {
      this.post().likes--;
      this.post().isLiked = false;
    } else {
      this.post().likes++;
      this.post().isLiked = true;
    }
    this.loaderService.isDisabled = true;
    this.postService.LikePost(postid, this.userId).subscribe({
      complete: () => {
        this.loaderService.isDisabled = false;
      },
    });
  }
}
