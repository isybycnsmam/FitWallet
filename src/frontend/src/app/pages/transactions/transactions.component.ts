import { Component, OnInit } from '@angular/core';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { TransactionModel } from '../../models/transactions/transaction';
import { environment } from '../../../environments/environment';
import { PagedModel } from '../../models/paged';

@Component({
  selector: 'transactions',
  standalone: true,
  imports: [CommonModule, RouterModule, NgFor, NgIf],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.scss',
})
export class TransactionsComponent implements OnInit {
  constructor(private http: HttpClient, private router: Router) {}

  scrollObserverElement: Element;
  intersectionObserver: IntersectionObserver = new IntersectionObserver(
    (entries) => this.scrollObserverCallback(entries)
  );
  transactions: TransactionModel[] = [];
  page: number = -1;

  ngOnInit(): void {
    this.scrollObserverElement = document.querySelector('.seemore')!;
    this.intersectionObserver.observe(this.scrollObserverElement!);
  }

  scrollObserverCallback(entries: IntersectionObserverEntry[]): void {
    if (entries[0].intersectionRatio <= 0) return;
    this.page++;
    this.loadPage(this.page);
  }

  loadPage(pageIndex: number): void {
    console.debug('Loading page', pageIndex);

    this.http
      .get<PagedModel<TransactionModel>>(
        `${environment.apiUrl}/transactions?page=${pageIndex}`
      )
      .subscribe((page) => {
        if (!page.isNext) {
          console.debug('No more pages to load, last page was', pageIndex);
          this.intersectionObserver.unobserve(this.scrollObserverElement);
        }

        if (page.pageIndex !== 0 && !page?.pageCount) {
          this.router.navigate(['/transactions/add']);
          return;
        }

        this.transactions.push(...page.data);
      });
  }

  deleteTransaction(id: string): void {
    if (!confirm('Czy na pewno usunąć?')) return;

    this.http.delete(`${environment.apiUrl}/transactions/${id}`).subscribe({
      next: () =>
        (this.transactions = this.transactions.filter((t) => t.id !== id)),
      error: () => {
        this.router.navigate(['/errorpage']);
      },
    });
  }

  toggleTransaction(id: string): void {
    let transaction = this.transactions.find((t) => t.id === id);
    if (!transaction) return;
    transaction.showElements = !transaction.showElements;
  }

  sumTransaction(transaction: TransactionModel): number {
    const sum = transaction.elements.reduce(
      (total, element) => total + element.value,
      0
    );
    return Number(sum.toFixed(2));
  }
}
