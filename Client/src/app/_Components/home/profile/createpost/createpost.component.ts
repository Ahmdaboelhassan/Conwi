import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogTitle,
  MatDialogRef,
} from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { User } from 'src/app/_models/User';
import { AuthService } from 'src/app/_services/auth.service';
import { PostService } from 'src/app/_services/post.service';
@Component({
  selector: 'app-createpost',
  standalone: true,
  imports: [
    FormsModule,
    MatDialogTitle,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
    MatButtonModule,
  ],
  templateUrl: './createpost.component.html',
  styleUrl: './createpost.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreatepostComponent implements OnInit {
  textAreaAvailbleChars = 100;
  textAreaMaxChars = 100;
  postImg: File | null = null;
  user: User;

  constructor(
    private readonly dialogRef: MatDialogRef<CreatepostComponent>,
    private readonly postService: PostService,
    private readonly authService: AuthService,
    private readonly toastrService: ToastrService
  ) {}

  ngOnInit(): void {
    this.authService.user$.pipe(take(1)).subscribe({
      next: (userEmitted) => (this.user = userEmitted),
      error: () => console.error('Faild To Load User'),
    });

    this.checkTextareaLength();
  }
  CloseDialog() {
    this.dialogRef.close();
  }

  chooseImg(e) {
    const img = e.target.files[0];
    if (img) this.postImg = img;
  }

  SubmitForm(form: NgForm) {
    const formData = new FormData();
    formData.append('content', form.value['content']);
    formData.append('photo', this.postImg);
    formData.append('userId', this.user.Id);

    this.postService.CreatePost(formData).subscribe({
      next: (res) => {
        form.reset();
        this.CloseDialog();
        this.toastrService.success('Post Has Been Posted Successfully');
      },
      error: (err) => console.log(err),
    });
  }

  private checkTextareaLength() {
    let textarea = document.getElementsByTagName('textarea')[0];
    textarea.addEventListener('input', () => {
      this.textAreaAvailbleChars =
        this.textAreaMaxChars - textarea.value.length;
    });
  }
}
