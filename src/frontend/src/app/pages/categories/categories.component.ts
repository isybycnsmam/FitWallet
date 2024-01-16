import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { CategoryModel } from '../../models/categories/category';

@Component({
  selector: 'categories',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './categories.component.html',
  styleUrl: './categories.component.scss',
})
export class CategoriesComponent implements OnInit {
  constructor(private http: HttpClient, private router: Router) {}

  categories: CategoryModel[] = [];

  ngOnInit(): void {
    this.http
      .get<CategoryModel[]>(`${environment.apiUrl}/categories`)
      .subscribe((data) => {
        if (!data?.length) {
          this.router.navigate(['/categories/add']);
          return;
        }

        this.categories = data;
      });
  }

  deleteCategory(id: string) {
    if (!confirm('Czy na pewno usunąć?')) return;

    this.http.delete(`${environment.apiUrl}/categories/${id}`).subscribe({
      next: () =>
        (this.categories = this.categories.filter((c) => c.id !== id)),
      error: () => this.router.navigate(['/errorpage']),
    });
  }
}
