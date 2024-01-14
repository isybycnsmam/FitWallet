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

  isUserLoggedIn: boolean = this.authService.isLoggedIn();
  username: string = 'great emperor';

  ngOnInit(): void {
    this.authService.tokenModifiedSubject.subscribe(() => {
      this.isUserLoggedIn = this.authService.isLoggedIn();
    });
  }

  logout() {
    this.authService.removeToken();
    this.router.navigate(['/login']);
  }
}
