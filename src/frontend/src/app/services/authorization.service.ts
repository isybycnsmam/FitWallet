import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationService {
  private jwtHelper: JwtHelperService;
  tokenModifiedSubject = new BehaviorSubject<boolean>(false);

  constructor() {
    this.jwtHelper = new JwtHelperService();
  }

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

  public getClaim(claimKey: string): any {
    const token = this.getToken();
    const decodedToken = this.jwtHelper.decodeToken(token!);
    return decodedToken ? decodedToken[claimKey] : null;
  }

  public getUserName(): string {
    return this.getClaim('http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname');
  }
}
