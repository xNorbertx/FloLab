import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';
import { Months } from '../_util/months';
import '../_util/date.extensions';
import { AuthService } from '../_services/auth/auth.service';
import { BookingService } from '../_services/booking/booking.service';
import { Selection } from '../_models/selection';
import { WeekCalendarComponent } from '../week-calendar/week-calendar.component';


@Component({
  selector: 'booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements AfterViewInit {

    @ViewChild(WeekCalendarComponent) weekCalendar;
    
    bookings: Selection[];

    constructor (
        private bookingService: BookingService,
        private auth: AuthService
    ) { }

    ngAfterViewInit() {
        this.getBookings();
    }

    getBookings() {
        this.bookingService.getBookingsPerWeek(this.weekCalendar.year, this.weekCalendar.week).subscribe(
            data => {
                this.bookings = data;
            },
            err => {
                console.log(err);
            }
        );
    }
}
