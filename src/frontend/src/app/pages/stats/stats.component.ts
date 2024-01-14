import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from '../../../environments/environment';
import { WalletStatisticsModel } from '../../models/wallets/wallet-statistics';
import { CommonModule, NgFor } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { StatisticsPieChartComponent } from '../../shared/statistics-pie-chart/statistics-pie-chart.component';

@Component({
  selector: 'app-stats',
  standalone: true,
  templateUrl: './stats.component.html',
  styleUrl: './stats.component.scss',
  imports: [CommonModule, NgFor, StatisticsPieChartComponent, RouterModule],
})
export class StatsComponent implements OnInit {
  constructor(private http: HttpClient, private router: Router) {}

  statisticsModels: WalletStatisticsModel[] = [];

  ngOnInit(): void {
    this.http
      .get<WalletStatisticsModel[]>(`${environment.apiUrl}/statistics/wallets`)
      .subscribe((data) => {
        if (!data?.length) {
          this.router.navigate(['/wallets/add']);
          return;
        }

        if (data.length === 1) {
          this.statisticsModels = data;
          return;
        }

        const summaryWallet: WalletStatisticsModel = data.reduce(
          (prev, current) => {
            return {
              id: 'summary',
              name: 'Podsumowanie',
              categories: [...prev.categories, ...current.categories],
              totalSpendings: prev.totalSpendings + current.totalSpendings,
            };
          }
        );

        this.statisticsModels = [summaryWallet, ...data];
      });
  }
}
