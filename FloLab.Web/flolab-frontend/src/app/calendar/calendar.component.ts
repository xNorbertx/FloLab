import { Component, OnInit } from '@angular/core';
import { Months } from '../_util/months';
import { BookingService } from '../_services/booking/booking.service';
import { UserService } from '../_services/user/user.service';
import { AuthService } from '../_services/auth/auth.service';
import { AttendanceService } from '../_services/attendance/attendance.service';
import { ModalService } from '../_services/modal/modal.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {
    monthDisplay: string;
    month: number;
    currentMonth: number;
    year: number;
    numberOfDatesSelected: number;
    selectedDates: string[];

    userDays: number[];

    constructor(
        private bookingService: BookingService,
        private userService: UserService,
        private authService: AuthService,
        private attendanceService: AttendanceService,
        private modal: ModalService
    ) { }

  ngOnInit() {
    const userId = this.authService.getId();

    //Get user days
    this.userService.getUserDays(userId).subscribe(
      data => {
        this.userDays = data;
      }
    )

    this.attendanceService.getAttendancesPerUser(userId).subscribe(
      data => {
        this.attendanceService.attendancesPerUser = data;

        //Set current date values
        const curJsDate = new Date();
        this.month = this.currentMonth = curJsDate.getMonth();
        this.monthDisplay = Months[this.month];
        this.year = curJsDate.getFullYear();

        //Execute main function
        this.calculateMonth(this.year, this.month);
      }
    );
  }

  //** Main function **
  calculateMonth(year: number, month: number): void {
    //Prepare variables in order to display days in month
    const firstDayOfMonth = ((new Date(year, month)).getDay() === 0 ? 7 : (new Date(year, month)).getDay());
    const daysOfMonth = this.daysInMonth(month, year);
    var datefield, datefields = document.getElementsByClassName("date-field");
    //Remove old values
    this.clearInnerHTML(datefields);
    this.removeClickEvent();
    //Display days in month
    for (var i = 1; i < daysOfMonth + 1; i++) {
      datefield = datefields[i + firstDayOfMonth - 2]; //-2 because i starts with 1 and because firstDayOfMonth is also not 0-based
      datefield.innerHTML = i.toString();
      if ((firstDayOfMonth + i) % 7 < 2) {
        datefield.classList.add('weekend');
      } else {
        datefield.classList.add('active');
      }
    }
    const attendancesCurrentMonth = this.attendanceService.attendancesPerUser.filter(x => new Date(x.date).getMonth() === month);
    let attendanceDay;
    for (var i = 0; i < attendancesCurrentMonth.length; i++) {
      attendanceDay = new Date(attendancesCurrentMonth[i].date).getDate();
      datefields[attendanceDay + firstDayOfMonth - 2].classList.add('attendance');
    }
    //Set click events on all divs with active class
    this.setClickEvent();
  }

  /* Handle click events on dates */
  clickListener: EventListenerOrEventListenerObject;

  setClickEvent(): void {
    var clickableElements = document.getElementsByClassName('active');
    var self = this;
    this.clickListener = this.setActiveDate.bind(self);
    for (let i = 0; i < clickableElements.length; i++) {
      clickableElements[i].addEventListener('click', this.clickListener);
    }
  }

  removeClickEvent(): void {
    var clickableElements = document.getElementsByClassName('date-field');
    for (let i = 0; i < clickableElements.length; i++) {
      clickableElements[i].removeEventListener('click', this.clickListener);
    }
  }

  //Misschien overbodig
  setActiveDate(e): void {

    this.modal.showModal();
  }

  /* Handle next or previous month button click */
  nextMonth(month: number, year: number) {
    if (this.month + 1 < 12) {
      this.month = month + 1;
    } else {
      this.month = 0; //zero-based
      this.year = year + 1;
    }
    this.monthDisplay = Months[this.month];
    this.calculateMonth(this.year, this.month);
  }

  previousMonth(month: number, year: number) {
    if (this.month - 1 > 0) {
      this.month = month - 1;
    } else {
      this.month = 11; //zero-based
      this.year = year - 1;
    }
    this.monthDisplay = Months[this.month];
    this.calculateMonth(this.year, this.month);
  }

  /* Helper functions */
  daysInMonth(month: number, year: number) {
    return 32 - new Date(year, month, 32).getDate();
  }
   

  clearInnerHTML = fields => {
    var i = 0;
    while (i < fields.length) {
      fields[i].innerHTML = "";
      fields[i].classList.remove('active');
      fields[i].classList.remove('weekend');
      fields[i].classList.remove('attendance');
      i++;
    }
  };
}
