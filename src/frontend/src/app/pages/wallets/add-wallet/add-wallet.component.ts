import { Component } from '@angular/core';
import { InputComponent } from '../../../shared/input/input.component';
import { HttpClient } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { environment } from '../../../../environments/environment';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-wallet',
  standalone: true,
  templateUrl: './add-wallet.component.html',
  styleUrl: './add-wallet.component.scss',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    RouterModule,
    InputComponent,
  ],
})
export class AddWalletComponent {
  constructor(private http: HttpClient, private router: Router) {}

  addWalletForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(20),
    ]),
    description: new FormControl('', [Validators.maxLength(300)]),
  });

  goback() {
    this.router.navigate(['/wallets']);
  }

  submitAdd() {
    const values = this.addWalletForm.value;
    return this.http
      .post(`${environment.apiUrl}/wallets`, {
        name: values.name,
        description: values.description,
      })
      .subscribe({
        next: (response: any) =>
          this.router.navigate(['/wallets', response.id]),
        error: (err) => {
          this.addWalletForm.controls['name'].setErrors({ incorrect: true });
        }
      });
  }
}
