import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AuthService } from '../../_services/auth.service';
import { LoaderService } from '../../_services/loader.service';
import { LoaderComponent } from '../loader/loader.component';

@Component({
  selector: 'app-Register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.scss'],
  imports: [ReactiveFormsModule, CommonModule, LoaderComponent],
  standalone: true,
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  showValiditionErrors: Boolean = false;
  isloading: boolean = false;
  constructor(
    private authService: AuthService,
    private loaderService: LoaderService
  ) {}

  ngOnInit(): void {
    this.intializeForm();
    this.loaderService.isLoading$.subscribe({
      next: (isloadingResult: boolean) => (this.isloading = isloadingResult),
    });
  }

  intializeForm() {
    this.registerForm = new FormGroup({
      FirstName: new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-z]*'),
      ]),
      LastName: new FormControl('', [
        Validators.required,
        Validators.pattern('[a-zA-z]*'),
      ]),
      Username: new FormControl('', [
        Validators.required,
        Validators.maxLength(15),
      ]),
      Email: new FormControl('', [Validators.required, Validators.email]),
      PhoneNumber: new FormControl('', [
        Validators.required,
        Validators.pattern('[0-9]*'),
      ]),
      Country: new FormControl('', Validators.required),
      City: new FormControl('', Validators.required),
      Password: new FormControl('', Validators.required),
      ConfrimPassword: new FormControl('', [
        Validators.required,
        this.matchValidaton('Password'),
      ]),
      DateOfBirth: new FormControl(
        new Date().toISOString().substring(0, 10),
        Validators.required
      ),
    });

    this.registerForm.get('Password').valueChanges.subscribe({
      next: () =>
        this.registerForm.get('ConfrimPassword').updateValueAndValidity(),
    });
  }

  matchValidaton(controlName: string): ValidatorFn {
    return (conroll: AbstractControl) => {
      return conroll.value == conroll.parent?.get(controlName)?.value
        ? null
        : { NotMatch: true };
    };
  }

  isNotValid(formControlNames) {
    let control: AbstractControl = this.registerForm.get(formControlNames);
    return !control.valid && this.showValiditionErrors;
  }

  register() {
    this.showValiditionErrors = true;
    if (this.registerForm.valid) {
      this.authService.register(this.registerForm.value);
    }
  }
}
