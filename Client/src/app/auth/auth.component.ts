import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormControl,
  FormGroup,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { AuthService } from '../Services/auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss'],
})
export class AuthComponent implements OnInit {
  registerForm: FormGroup;
  errors: string[];
  emptyString: string = '';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.intializeForm();

    this.authService.errors$.subscribe({
      next: (errorList) => (this.errors = errorList),
    });
  }

  intializeForm() {
    this.registerForm = new FormGroup({
      FirstName: new FormControl(this.emptyString, [
        Validators.required,
        Validators.pattern('[a-zA-z]*'),
      ]),
      LastName: new FormControl(this.emptyString, [
        Validators.required,
        Validators.pattern('[a-zA-z]*'),
      ]),
      Username: new FormControl(this.emptyString, [
        Validators.required,
        Validators.maxLength(15),
      ]),
      Email: new FormControl(this.emptyString, [
        Validators.required,
        Validators.email,
      ]),
      PhoneNumber: new FormControl(this.emptyString, [
        Validators.required,
        Validators.pattern('[0-9]*'),
      ]),
      Country: new FormControl(this.emptyString, Validators.required),
      City: new FormControl(this.emptyString, Validators.required),
      Password: new FormControl(this.emptyString, Validators.required),
      ConfrimPassword: new FormControl(this.emptyString, [
        Validators.required,
        this.matchValidaton('Password'),
      ]),
      DateOfBirth: new FormControl(
        new Date(1990, 1, 1).toISOString().substring(0, 10),
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

  isNotValid(formControlName) {
    let control: AbstractControl = this.registerForm.get(formControlName);
    return !control.valid && control.touched;
  }

  register() {
    return this.authService.register(this.registerForm.value);
  }
}
