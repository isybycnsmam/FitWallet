import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormGroup,
  FormControl,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { CategoryModel } from '../../../models/categories/category';
import { InputComponent } from '../../../shared/input/input.component';
import { WalletModel } from '../../../models/wallets/wallet';
import { NgFor } from '@angular/common';
import { TransactionElementModel } from '../../../models/transactions/transaction-element';
import { TransactionModel } from '../../../models/transactions/transaction';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-add-edit-transaction',
  standalone: true,
  templateUrl: './add-edit-transaction.component.html',
  styleUrl: './add-edit-transaction.component.scss',
  imports: [InputComponent, RouterModule, ReactiveFormsModule, FormsModule, NgFor],
})
export class AddEditTransactionComponent implements OnInit {
  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  id: string | null;
  isEdit: boolean = false;
  transactionform = new FormGroup({
    wallet: new FormControl('', [Validators.required]),
    description: new FormControl('', []),
    who: new FormControl('', [Validators.required]),
    date: new FormControl('', [Validators.required]),
  });

  categories: CategoryModel[] = [];
  wallets: WalletModel[] = [];
  companies: string[] = [];

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.isEdit = !!this.id;

    this.http
      .get<CategoryModel[]>(`${environment.apiUrl}/categories`)
      .subscribe({
        next: (response) => (this.categories = response),
        error: (err) => this.router.navigate(['/errorpage']),
      });

    this.http.get<WalletModel[]>(`${environment.apiUrl}/wallets`).subscribe({
      next: (response) => (this.wallets = response),
      error: (err) => this.router.navigate(['/errorpage']),
    });

    if (this.id) {
      this.http
        .get<TransactionModel>(`${environment.apiUrl}/transactions/${this.id}`)
        .subscribe({
          next: (response) => {
            console.log(response);
            this.transactionform.controls['wallet'].setValue(response.walletId);
            this.transactionform.controls['description'].setValue(
              response.description
            );
            this.transactionform.controls['who'].setValue(response.companyName);
            this.transactionform.controls['date'].setValue(formatDate(response.operationDate, 'yyyy-MM-dd', 'pl'));
          },
          error: (err) => this.router.navigate(['/errorpage']),
        });
    }
  }

  goback() {
    this.router.navigate(['/wallets']);
  }

  submitAdd() {
    // const values = this.addCategoryForm.value;
    // const method = this.isEdit ? 'put' : 'post';
    // return this.http
    //   .request(
    //     method,
    //     `${environment.apiUrl}/categories/${this.isEdit ? this.id : ''}`,
    //     {
    //       body: {
    //         name: values.name,
    //         displayColor: parseInt(values.color!.replace('#', ''), 16),
    //       },
    //     }
    //   )
    //   .subscribe({
    //     next: (response: any) => this.router.navigate(['/categories']),
    //     error: (err) => {
    //       this.addCategoryForm.controls['name'].setErrors({ incorrect: true });
    //     },
    //   });
  }
}
