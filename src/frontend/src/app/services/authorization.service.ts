import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  saveToken(token: { accessToken: string }) {
    localStorage.setItem('jwt_token', token.accessToken);
  }

  getToken() {
    return localStorage.getItem('jwt_token');
  }

  removeToken() {
    localStorage.removeItem('jwt_token');
  }

  isLoggedIn() {
    return this.getToken() !== null;
  }
}
