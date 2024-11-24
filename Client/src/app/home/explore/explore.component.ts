import { Component, OnInit } from '@angular/core';
import { UsersComponent } from './users/users.component';
import { LoaderComponent } from 'src/app/loader/loader.component';
import { switchMap } from 'rxjs';
import { UserService } from 'src/app/_services/user.service';
import { AuthService } from 'src/app/_services/auth.service';
import { UserCard } from 'src/app/_interface/Response/UserCard';

@Component({
  selector: 'app-explore',
  standalone: true,
  imports: [UsersComponent, LoaderComponent],
  templateUrl: './explore.component.html',
  styleUrl: './explore.component.scss',
})
export class ExploreComponent implements OnInit {
  users: UserCard[] = [];
  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {}
  ngOnInit(): void {
    this.authService.user$
      .pipe(switchMap((user) => this.userService.ExploreUsers(user.Id)))
      .subscribe((users) => (this.users = users));
  }
  Search(e: any) {
    this.authService.user$
      .pipe(
        switchMap((user) =>
          this.userService.SearchUsers(e.target.value, user.Id)
        )
      )
      .subscribe((users) => (this.users = users));
    console.log(this.users);
  }
}
