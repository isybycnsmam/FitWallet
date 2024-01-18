import { TransactionElementModel } from "./transaction-element";

export class TransactionModel {
  id: string;
  description: string;
  operationDate: Date;
  walletId: string;
  walletName: string;
  companyName: string;
  elements: TransactionElementModel[];
}
