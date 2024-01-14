import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  FormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Router, RouterModule } from '@angular/router';
import { InputComponent } from '../../shared/input/input.component';
import { AuthorizationService } from '../../services/authorization.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    InputComponent,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthorizationService
  ) {}

  registerForm = new FormGroup({
    firstname: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(15),
    ]),
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(25),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(30),
    ]),
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  submitRegister() {
    const values = this.registerForm.value;
    this.http
      .post(`${environment.apiUrl}/identity/register`, {
        firstname: values.firstname,
        username: values.username,
        password: values.password,
        email: values.email,
      })
      .subscribe({
        next: (response) => this.registerSuccessfull(response),
        error: (err) => this.handleError(err),
      });
  }

  handleError(response: any) {
    // var errors: { [key: string]: string[] } = response.error.errors;
    // var errorMessage = '';
    // if (errors['PasswordRequiresNonAlphanumeric'])
    //   errorMessage += "Hasło musi zawierać co najmniej jeden znak specjalny";
    // if (errors['PasswordRequiresUpper'])
    //   errorMessage += 'Hasło musi zawierać co najmniej jedną wielką literę (A-Z).';
    // if (errors['PasswordRequiresDigit'])
    //   errorMessage += 'Hasło musi zawierać co najmniej jedną cyfrę (0-9).';
    // if (errors['PasswordRequiresLower'])
    //   errorMessage += 'Hasło musi zawierać co najmniej jedną małą literę (a-z).';
    // if (errors['PasswordRequiresUniqueChars'])
    //   errorMessage += 'Hasło musi zawierać co najmniej 5 unikalnych znaków.';
    // if (errors['PasswordTooShort'])
    //   errorMessage += 'Hasło musi zawierać co najmniej 8 znaków.';
    // this.registerForm.controls['password'].setErrors({ incorrect: true });
  }

  registerSuccessfull(response: any): void {
    this.authService.saveToken({ accessToken: response.accessToken });
    this.router.navigate(['/how-to']);
  }
}
