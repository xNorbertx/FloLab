import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Reservation } from '../../#models/reservation';
import { map, catchError } from 'rxjs/operators';
import { ErrorService } from '../Error/error.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(
    private http: HttpClient,
    private error: ErrorService
  ) { }

  getReservationsPerUser(userId: string): Observable<Reservation[]> {
    return this.http.get<Reservation[]>(`api/reservation?UserId=${userId}`, httpOptions)
      .pipe(
        map(data => {
          return data;
        }),
        catchError(this.error.handleError)
      );
  }
}
