import { Component, Input, OnInit } from '@angular/core';
import { ChartData, ChartOptions } from 'chart.js';
import { NgChartsModule } from 'ng2-charts';
import { WalletStatisticsModel } from '../../models/wallets/wallet-statistics';
import { NgFor } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-statistics-pie-chart',
  standalone: true,
  imports: [NgChartsModule, NgFor, RouterModule],
  templateUrl: './statistics-pie-chart.component.html',
  styleUrl: './statistics-pie-chart.component.scss',
})
export class StatisticsPieChartComponent implements OnInit {
  pieChartData: ChartData<'pie', number[], string | string[]>;
  pieChartOptions: ChartOptions<'pie'>;

  leftPanelButtons: { text: string; color: string, onClickLink: string }[] = [];
  rightPanelButtons: { text: string; color: string, onClickLink: string }[] = [];

  @Input() model: WalletStatisticsModel;
  @Input() borderColor: string = `#000`;

  ngOnInit(): void {
    this.initializeChart();
    this.initializeButtons();
  }

  initializeChart() {
    const labels = this.model.categories.map((c) => c.name);
    const data = this.model.categories.map((c) => c.totalSpendings);
    const colors = this.model.categories.map(
      (c) => this.getHexColor(c.displayColor)
    );

    this.pieChartOptions = {
      responsive: false,
      borderColor: this.borderColor,
    };

    this.pieChartData = {
      labels: labels,
      datasets: [
        {
          data: data,
          backgroundColor: colors,
        },
      ],
    };
  }

  initializeButtons() {
    const totalSpendings = this.model.categories.reduce(
      (sum, category) => sum + category.totalSpendings,
      0
    );

    const buttonsData = this.model.categories.map((category) => {
      const percentage = Math.round(
        (category.totalSpendings / totalSpendings) * 100
      );

      return {
        text: `${percentage}% - ${category.name}`,
        onClickLink: '', //`/${category.id}`,
        color: this.getHexColor(category.displayColor),
      };
    });

    const midpoint = Math.ceil(buttonsData.length / 2);

    this.leftPanelButtons = buttonsData.slice(0, midpoint);
    this.rightPanelButtons = buttonsData.slice(midpoint);
  }

  getHexColor(color: number) {
    return `#${color.toString(16)}`;
  }
}
