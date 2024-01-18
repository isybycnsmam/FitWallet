import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

@Component({
  selector: 'app-nav-bar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './nav-bar.component.html',
  styleUrl: './nav-bar.component.scss',
})
export class NavBarComponent implements OnInit {
  constructor(
    private authService: AuthorizationService,
    private router: Router
  ) {}

  isUserLoggedIn: boolean;
  username: string;

  ngOnInit(): void {
    this.authService.tokenModifiedSubject.subscribe(() =>
      this.updateUserStatus()
    );
  }

  updateUserStatus() {
    this.isUserLoggedIn = this.authService.isLoggedIn();
    if (this.isUserLoggedIn) {
      this.username = this.authService.getUserName();
    }
  }

  logout() {
    this.authService.removeToken();
    this.router.navigate(['/login']);
  }
}
