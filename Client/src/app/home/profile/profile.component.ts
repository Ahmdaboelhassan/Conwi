import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild, inject } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { take } from 'rxjs';
import { UserProfile } from 'src/app/Interfaces/Response/UserProfile';
import { PostComponent } from 'src/app/post/post.component';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { environment } from 'src/environments/environment.development';
import { MatDialog } from '@angular/material/dialog';
import { CreatepostComponent } from './createpost/createpost.component';
import { IconService } from 'src/app/Services/Icons.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-profle',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  imports: [PostComponent, CommonModule, FormsModule, FontAwesomeModule],
  standalone: true,
})
export class ProfileComponent implements OnInit {
  @ViewChild('postForm', { static: false }) postForm: NgForm;
  user: UserProfile = null;
  defualtPhoto = environment.defualtProfilePhoto;
  icons;

  constructor(
    private userService: UserService,
    private authService: AuthService,
    private iconServce: IconService,
    private matDialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.icons = this.iconServce.icons;
    this.intializeUserProfile(this.getUserEmail());
  }

  private getUserEmail() {
    let userEmail: string;
    this.authService.user$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) userEmail = user.Email;
      },
    });
    return userEmail;
  }

  private intializeUserProfile(email: string) {
    if (email) {
      this.userService.GetUserProfile(email).subscribe({
        next: (profile) => {
          let userProfile: UserProfile = {
            firstName: profile.firstName,
            lastName: profile.lastName,
            country: profile.country,
            city: profile.city,
            email: profile.email,
            userName: profile.userName,
            dateOfBirth: profile.dateOfBirth,
            userPosts: profile.userPosts,
            photoURL: profile.photoURL,
          };
          this.user = userProfile;

          if (!this.user.photoURL) {
            this.user.photoURL = this.defualtPhoto;
          }
        },
        error: (err) => console.log(err.message),
      });
    }
  }

  openDialog() {
    this.matDialog.open(CreatepostComponent);
  }
}
