import { Injectable } from '@angular/core';

import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ErrorService } from '../error/error.service';
import { catchError, map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class UserService {



  constructor(
    private http: HttpClient,
    private error: ErrorService
  ) { }

  getUserDays(userId: string): Observable<number[]> {
    return this.http.get<number[]>(`/api/user/getdays?UserId=${userId}`, httpOptions)
      .pipe(
        map(data => {
          return data;
        }),
        catchError(this.error.handleError)
      );
  }
}
