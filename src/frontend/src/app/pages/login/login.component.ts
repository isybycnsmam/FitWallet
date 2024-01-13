import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormGroup,
  FormControl,
  ReactiveFormsModule,
  FormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Router } from '@angular/router';
import { InputComponent } from "../../shared/input/input.component";

@Component({
    selector: 'app-login',
    standalone: true,
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss',
    imports: [CommonModule, ReactiveFormsModule, FormsModule, InputComponent]
})
export class LoginComponent {
  constructor(private http: HttpClient, private router: Router) {}

  loginFailed = false;
  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });

  submitLogin() {
    const values = this.loginForm.value;
    this.http
      .post(`${environment.apiUrl}/identity/login`, {
        username: values.username,
        password: values.password,
      })
      .subscribe({
        next: (response) => this.loginSuccessfull(response),
        error: () => this.loginFailed = true
      });
  }

  loginSuccessfull(response: any) {
    this.loginFailed = false;
    localStorage.setItem('jwt_token', response.accessToken);
    this.router.navigate(['/how-to']);
  }
}
