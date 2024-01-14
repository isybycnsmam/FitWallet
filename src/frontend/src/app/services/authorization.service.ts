import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  tokenModifiedSubject = new BehaviorSubject<boolean>(false);

  saveToken(token: { accessToken: string }) {
    localStorage.setItem('jwt_token', token.accessToken);
    this.tokenModifiedSubject.next(this.isLoggedIn());
  }

  getToken() {
    return localStorage.getItem('jwt_token');
  }

  removeToken() {
    localStorage.removeItem('jwt_token');
    this.tokenModifiedSubject.next(this.isLoggedIn());
  }

  isLoggedIn() {
    return this.getToken() !== null;
  }
}
