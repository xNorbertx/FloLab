import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Selection } from '../../_models/selection';
import { map, catchError } from 'rxjs/operators';
import { ErrorService } from '../error/error.service';
import { ReturnMessage } from '../../_models/returnMessage';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(
    private http: HttpClient,
    private error: ErrorService
  ) { }

    getBookingsPerWeek(year: number, week: number): Observable<Selection[]> {
        return this.http.get<Selection[]>(`api/booking?year=${year}&week=${week}`, httpOptions)
            .pipe(
                map(data => {
                    return data;
                }),
                catchError(this.error.handleError)
            );
    }

    insertBooking(selection: Selection): Observable<string> {
        return this.http.post<ReturnMessage>(`api/booking`, selection, httpOptions)
            .pipe(
                map(data => {
                    return data.message;
                }),
                catchError(this.error.handleError)
            );
    }

    deleteBooking(bookingId: number): Observable<string> {
        return this.http.delete<ReturnMessage>(`api/booking?id=${bookingId}`, httpOptions)
            .pipe(
                map(data => {
                    return data.message;
                }),
                catchError(this.error.handleError)
            );
    }

}
