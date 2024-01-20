import { CategoryStatisticsModel } from "../categories/category-statistics";

export class WalletStatisticsModel {
  id: string;
  name: string;
  categories: CategoryStatisticsModel[];
  totalSpendings: number;
}
