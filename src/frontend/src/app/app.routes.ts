import { Routes } from '@angular/router';
import { HowToPageComponent } from './pages/how-to/how-to-page.component';
import { LoginPageComponent } from './pages/login/login-page.component';

export const routes: Routes = [
  { path: '', redirectTo: 'how-to', pathMatch: 'full' },
  { path: 'how-to', component: HowToPageComponent},
  { path: 'login', component: LoginPageComponent},
];
