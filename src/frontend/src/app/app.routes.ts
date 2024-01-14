import { Routes } from '@angular/router';
import { HowToComponent } from './pages/how-to/how-to.component';
import { LoginComponent } from './pages/login/login.component';
import { ContactComponent } from './pages/contact/contact.component';
import { RegisterComponent } from './pages/register/register.component';
import { onlyLoggedUsersGuard, onlyNotLoggedUsersGuard } from './guards/auth.guard';
import { WalletsComponent } from './pages/wallets/wallets.component';
import { ErrorPageComponent } from './pages/errorpage/errorpage.component';
import { AdminPageComponent } from './pages/adminpage/adminpage.component';

export const routes: Routes = [
  { path: '', redirectTo: 'how-to', pathMatch: 'full' },
  { path: 'how-to', component: HowToComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'errorpage', component: ErrorPageComponent },
  { path: 'adminpage', component: AdminPageComponent }, //dodaj tutaj uprawnienia dla admina
  { path: 'register', component: RegisterComponent, canActivate: [onlyNotLoggedUsersGuard] },
  { path: 'login', component: LoginComponent, canActivate: [onlyNotLoggedUsersGuard] },
  { path: 'wallets', component: WalletsComponent, canActivate: [onlyLoggedUsersGuard]},
  { path: '**', redirectTo: 'how-to' }
];
