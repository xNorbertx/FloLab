import { Component, OnInit, Input } from '@angular/core';
import { Months } from '../calendar/util-calendar';
import { FormGroup, FormBuilder, Validators, FormArray } from '@angular/forms';
import { BookingService } from '../#services/booking/booking.service';

@Component({
  selector: 'booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.scss']
})
export class BookingComponent implements OnInit {
  private _nrDatesSelected: number;
  @Input() set nrDatesSelected(value: number) {
    this._nrDatesSelected = value;
  }
  get nrDatesSelected(): number {
    return this._nrDatesSelected;
  }

  constructor (
    private formBuilder: FormBuilder,
    private bookingService: BookingService
  ) { }

  ngOnInit() {
    this.bookingService.createForm();
  }

  finishBooking(): void {

  }
}
