import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
})
export class NavBarComponent {
  constructor(private authService: AuthorizationService) {}

  username: string = 'great emperor';

  isUserLoggedIn = this.authService.isLoggedIn();

  logout() {
    this.authService.removeToken();
    window.location.reload();
  }
}
