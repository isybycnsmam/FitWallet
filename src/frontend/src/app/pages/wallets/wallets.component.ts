import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Chart, ChartOptions } from 'chart.js';
import { NgChartsModule } from 'ng2-charts';
import { environment } from '../../../environments/environment';
import { WalletModel } from '../../models/wallets/wallet';

@Component({
  selector: 'app-wallets',
  standalone: true,
  imports: [NgChartsModule],
  templateUrl: './wallets.component.html',
  styleUrl: './wallets.component.scss',
})
export class WalletsComponent implements OnInit {
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<WalletModel[]>(`${environment.apiUrl}/wallets`)
      .subscribe((data) => {
        data.forEach((wallet) => {
          this.pieChartLabels.push(wallet.name);
          this.pieChartDatasets[0].data.push(1);
          this.pieChartDatasets[0].data.push(1);
        });

        this.pieChartLabels = data.map((wallet) => wallet.name);
      });
  }

  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: false,
    borderColor: '#000',
  };
  pieChartLabels: string[] = [];
  public pieChartDatasets: { data: number[] }[] = [
    {
      data: [  ],
    },
  ];
  public pieChartLegend = true;
  public pieChartPlugins = [];
}
