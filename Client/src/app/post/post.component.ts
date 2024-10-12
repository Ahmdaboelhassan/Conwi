import { Component, input } from '@angular/core';
import { ReadPost } from 'src/app/Interfaces/Response/ReadPost';
import { environment } from 'src/environments/environment.development';

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
}
