import { Injectable } from '@angular/core';
import { FormArray, FormGroup, FormBuilder } from '@angular/forms';

@Injectable()
export class BookingService {
  bookingForm: FormGroup;
  bookingDates: FormArray;

  constructor(
    private formBuilder: FormBuilder
  ) { }

  createForm(): void {
    this.bookingForm = this.formBuilder.group({
      bookingDates: this.formBuilder.array([])
    });
  }

  addBookingDate(date: string): void {
    this.bookingDates = this.bookingForm.get('bookingDates') as FormArray;
    this.bookingDates.push(this.createBookingDate(date));
  }

  removeBookingDate(date: string): void {
    //to implement
  }

  createBookingDate(date: string): FormGroup {
    return this.formBuilder.group({
      date: date,
      remark: ''
    });
  }
}
