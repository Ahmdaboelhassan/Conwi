import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { take } from 'rxjs';
import { UserProfile } from 'src/app/Interfaces/UserProfile';
import { AuthService } from 'src/app/Services/auth.service';
import { UserService } from 'src/app/Services/user.service';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-profle',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  @ViewChild('postForm', { static: false }) postForm: NgForm;
  defualtPhoto = environment.defualtProfilePhoto;
  textAreaAvailbleChars = 100;
  textAreaMaxChars = 100;
  user: UserProfile;
  PostArea = false;

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.intializeUserProfile(this.getUserEmail());
  }
  enablePostArea() {
    this.PostArea = true;
    setTimeout(() => this.checkTextareaLength(), 500);
  }

  disablePostArea() {
    this.PostArea = false;
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
          };
          this.user = userProfile;
          if (this.user && !this.user.photoURL) {
            this.user.photoURL = this.defualtPhoto;
          }
        },
        error: (err) => console.log(err.message),
      });
    }
  }
  private checkTextareaLength() {
    let textarea = document.getElementsByTagName('textarea')[0];
    textarea.addEventListener('input', () => {
      this.textAreaAvailbleChars =
        this.textAreaMaxChars - textarea.value.length;
    });
  }
}
