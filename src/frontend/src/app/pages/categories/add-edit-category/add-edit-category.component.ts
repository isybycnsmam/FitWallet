import { Component, Input, OnInit } from '@angular/core';
import { InputComponent } from '../../../shared/input/input.component';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { NgIf } from '@angular/common';
import { CategoryModel } from '../../../models/categories/category';

@Component({
  selector: 'app-add-edit-category',
  standalone: true,
  templateUrl: './add-edit-category.component.html',
  styleUrl: './add-edit-category.component.scss',
  imports: [
    InputComponent,
    ReactiveFormsModule,
    FormsModule,
    NgIf,
    RouterModule,
  ],
})
export class AddEditCategoryComponent implements OnInit {
  constructor(
    private http: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  id: string | null;
  isEdit: boolean = false;
  addCategoryForm = new FormGroup({
    name: new FormControl('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(25),
    ]),
    color: new FormControl('', [Validators.required]),
  });

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    this.isEdit = !!this.id;

    if (this.id) {
      this.http
        .get<CategoryModel>(`${environment.apiUrl}/categories/${this.id}`)
        .subscribe({
          next: (response) => {
            this.addCategoryForm.controls['name'].setValue(response.name);
            this.addCategoryForm.controls['color'].setValue(
              '#' + response.displayColor.toString(16)
            );
          },
          error: (err) => this.router.navigate(['/errorpage']),
        });
    }
  }

  goback() {
    this.router.navigate(['/wallets']);
  }

  submitAdd() {
    const values = this.addCategoryForm.value;
    const method = this.isEdit ? 'put' : 'post';
    return this.http
      .request(
        method,
        `${environment.apiUrl}/categories/${this.isEdit ? this.id : ''}`,
        {
          body: {
            name: values.name,
            displayColor: parseInt(values.color!.replace('#', ''), 16),
          },
        }
      )
      .subscribe({
        next: (response: any) =>
          this.router.navigate(['/categories']),
        error: (err) => {
          this.addCategoryForm.controls['name'].setErrors({ incorrect: true });
        },
      });
  }
}
