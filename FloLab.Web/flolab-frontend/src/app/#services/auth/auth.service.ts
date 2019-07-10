import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map } from 'rxjs/operators';
import { Login } from '../../#models/login';
import { Observable } from 'rxjs';
import { ErrorService } from '../Error/error.service';
import decode from 'jwt-decode';
import { JwtHelperService } from '@auth0/angular-jwt';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private claims = {
    role: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role',
    nameIdentifier: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier',
    expiration: 'exp'
  }

  private setJwt(token: string): void {
    localStorage.setItem('token', token);
  }

  private getJwt(): string {
    return localStorage.getItem('token');
  }

  getRole(): string {
    const payload = decode(this.getJwt());
    return payload[this.claims.role];
  }

  getId(): string {
    const payload = decode(this.getJwt());
    return payload[this.claims.nameIdentifier];
  }

  public isAuthenticated(): boolean {
    let token = this.getJwt();
    if (token === '') {
      return false;
    }

    let isValidToken = !this.jwtHelper.isTokenExpired(token);
    return isValidToken;
  }

  constructor(
    private http: HttpClient,
    private error: ErrorService,
    private jwtHelper: JwtHelperService
  ) { }

  login(login: Login): Observable<string | {}> {
    return this.http.post<any>(`/api/auth/login`, { Email: login.email, Password: login.password }, httpOptions)
      .pipe(
        map(data => {
          if (data) {
            this.setJwt(data.token);
          }
        return data.token;
      }),
      catchError(this.error.handleError)
    );
  }
}
