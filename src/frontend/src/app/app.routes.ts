import { Routes } from '@angular/router';
import { HowToComponent } from './pages/how-to/how-to.component';
import { LoginComponent } from './pages/login/login.component';
import { ContactComponent } from './pages/contact/contact.component';
import { RegisterComponent } from './pages/register/register.component';
import {
  onlyLoggedUsersGuard,
  onlyNotLoggedUsersGuard,
} from './guards/auth.guard';
import { WalletsComponent } from './pages/wallets/wallets.component';
import { ErrorPageComponent } from './pages/errorpage/errorpage.component';
import { StatsComponent } from './pages/stats/stats.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';
import { CategoriesComponent } from './pages/categories/categories.component';
import { AdminPageComponent } from './pages/adminpage/adminpage.component';
import { AddWalletComponent } from './pages/wallets/add-wallet/add-wallet.component';
import { AddEditCategoryComponent } from './pages/categories/add-edit-category/add-edit-category.component';
import { AddEditTransactionComponent } from './pages/transactions/add-edit-transaction/add-edit-transaction.component';

export const routes: Routes = [
  { path: '', redirectTo: 'how-to', pathMatch: 'full' },
  { path: 'how-to', component: HowToComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'errorpage', component: ErrorPageComponent },
  { path: 'adminpage', component: AdminPageComponent },
  { path: 'stats', component: StatsComponent },

  // Transactions
  { path: 'transactions', component: TransactionsComponent },
  {
    path: 'transactions/add-edit',
    component: AddEditTransactionComponent,
    canActivate: [onlyLoggedUsersGuard],
  },
  {
    path: 'transactions/add-edit/:id',
    component: AddEditTransactionComponent,
    canActivate: [onlyLoggedUsersGuard],
  },

  // login & register
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [onlyNotLoggedUsersGuard],
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [onlyNotLoggedUsersGuard],
  },

  // wallets
  {
    path: 'wallets',
    component: WalletsComponent,
    canActivate: [onlyLoggedUsersGuard],
  },
  {
    path: 'wallets/add',
    component: AddWalletComponent,
    canActivate: [onlyLoggedUsersGuard],
  },

  // categories
  { path: 'categories', component: CategoriesComponent },
  {
    path: 'categories/add-edit',
    component: AddEditCategoryComponent,
    canActivate: [onlyLoggedUsersGuard],
  },
  {
    path: 'categories/add-edit/:id',
    component: AddEditCategoryComponent,
    canActivate: [onlyLoggedUsersGuard],
  },

  { path: '**', redirectTo: 'how-to' },
];
