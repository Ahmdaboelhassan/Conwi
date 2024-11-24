import { NgClass } from '@angular/common';
import { Component, input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserCard } from 'src/app/_interface/Response/UserCard';
import { LoaderService } from 'src/app/_services/loader.service';
import { UserService } from 'src/app/_services/user.service';
import { LoaderComponent } from 'src/app/loader/loader.component';
import { environment } from 'src/environments/environment.development';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [LoaderComponent, RouterLink, NgClass],
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss',
})
export class UsersComponent {
  defualtPhoto = environment.defualtProfilePhoto;
  users = input<UserCard[]>([]);
  isloading: boolean = false;

  constructor(
    private loaderService: LoaderService,
    private userService: UserService,
    private toasterService: ToastrService
  ) {}

  ngOnInit(): void {
    this.loaderService.isLoading$.subscribe({
      next: (isloadingResult: boolean) => (this.isloading = isloadingResult),
    });
  }

  FollowUser(user, e) {
    const followButton = e.target.parentElement;
    const userCard = followButton.closest('.user-card');
    const loader = followButton.querySelector('.loader');
    const text = followButton.querySelector('.text');
    const primaryLoading = document.getElementById('loader');

    loader.classList.remove('hidden');
    text.classList.add('hidden');
    primaryLoading.classList.add('opacity-0');

    var currentUser = JSON.parse(localStorage.getItem('user'));
    this.userService.followUser(currentUser.Id, user.userId).subscribe({
      next: () => {
        this.toasterService.success('follow Done Sucessfully');
      },
      error: (err) => console.log(err),
      complete: () => {
        primaryLoading.classList.remove('opacity-0');
        userCard.classList.add('hidden');
      },
    });
  }
}
