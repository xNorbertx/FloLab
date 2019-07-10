import { Injectable } from '@angular/core';
import { Attendance } from '../../#models/attendance';
import { Observable } from 'rxjs';
import { ErrorService } from '../Error/error.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
}

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  public attendancesPerUser: Attendance[];

  constructor(
    private http: HttpClient,
    private error: ErrorService
  ) { }

  getAttendancesPerUser(userId: string): Observable<Attendance[]> {
    return this.http.get<Attendance[]>(`api/attendance?UserId=${userId}`, httpOptions)
      .pipe(
        map(data => {
          return data;
        }),
        catchError(this.error.handleError)
      );
  }
}
