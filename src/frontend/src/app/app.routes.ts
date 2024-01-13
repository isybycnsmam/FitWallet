import { Routes } from '@angular/router';
import { HowToComponent } from './pages/how-to/how-to.component';
import { LoginComponent } from './pages/login/login.component';
import { ContactComponent } from './pages/contact/contact.component';

export const routes: Routes = [
  { path: '', redirectTo: 'how-to', pathMatch: 'full' },
  { path: 'how-to', component: HowToComponent},
  { path: 'login', component: LoginComponent},
  { path: 'contact', component: ContactComponent},
];
