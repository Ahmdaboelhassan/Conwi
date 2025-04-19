import { CommonModule, NgClass } from '@angular/common';
import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { take } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { MatDialog } from '@angular/material/dialog';
import { CreatepostComponent } from './createpost/createpost.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserProfile } from 'src/app/_interface/Response/UserProfile';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { LoaderService } from 'src/app/_services/loader.service';
import { PostComponent } from '../../post/post.component';
import { LoaderComponent } from '../../loader/loader.component';

@Component({
  selector: 'app-profle',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  imports: [
    PostComponent,
    CommonModule,
    FormsModule,
    FontAwesomeModule,
    LoaderComponent,
    NgClass,
  ],
  standalone: true,
})
export class ProfileComponent implements OnInit {
  @ViewChild('postForm', { static: false }) postForm: NgForm;
  defualtPhoto = environment.defualtProfilePhoto;
  userProfile: UserProfile = null;
  userProfileId: string;
  userId: string;
  isloading: boolean = false;
  isCuurentUser: boolean = false;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private matDialog: MatDialog,
    private loaderService: LoaderService,
    private activeRoute: ActivatedRoute,
    private toesterService: ToastrService
  ) {}

  ngOnInit(): void {
    this.userId = this.authService.getCurrentUserId();
    let id = this.activeRoute.snapshot.paramMap.get('userId');
    this.isCuurentUser = !id;

    this.userProfileId = id ? id : this.userId;
    this.intializeUserProfile(this.userProfileId);

    this.loaderService.isLoading$.subscribe({
      next: (isloadingResult: boolean) => (this.isloading = isloadingResult),
    });
  }

  private intializeUserProfile(id: string) {
    if (id) {
      this.userService.GetUserProfile(id, this.userId).subscribe({
        next: (profile) => {
          this.userProfile = profile;
          if (!this.userProfile.photoURL) {
            this.userProfile.photoURL = this.defualtPhoto;
          }
        },
        error: (err) => console.log(err.message),
      });
    }
  }

  openDialog() {
    this.matDialog.open(CreatepostComponent, { width: '45rem' });
  }

  UploadProfilePicture(e) {
    const profileImage = e.target.files[0];

    if (profileImage) {
      const imageFile: File | null = profileImage;

      const fd = new FormData();
      fd.append('userId', this.userId);
      fd.append('profilePhoto', imageFile);

      this.userService.UploadProfilePhoto(fd).subscribe({
        next: () => {
          document
            .getElementById('Profile')
            .setAttribute('src', window.URL.createObjectURL(profileImage));
        },
      });
    }
  }

  FollowUser(e) {
    this.userProfile.isFollowing = !this.userProfile.isFollowing;
    this.loaderService.isDisabled = true;
    this.userService.followUser(this.userId, this.userProfileId).subscribe({
      next: () => this.toesterService.success('Opertion Done Successfully'),
      complete: () => (this.loaderService.isDisabled = false),
    });
  }
}
