import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  registerUrl = `${environment.apiUrl}/register`;
  loginUrl = `${environment.apiUrl}/login`;

  constructor(private http: HttpClient) { }

  public register(user: any): Observable<any> {
    return this.http.post(this.registerUrl, user);
  }

  public login(user: any): Observable<any> {
    return this.http.post(this.loginUrl, user);
  }
}
