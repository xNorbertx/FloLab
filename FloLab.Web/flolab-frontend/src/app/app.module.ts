import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { MenubarComponent } from './menubar/menubar.component';
import { StartComponent } from './start/start.component';
import { CalendarComponent } from './calendar/calendar.component';
import { EventsComponent } from './events/events.component';
import { BookingComponent } from './booking/booking.component';
import { ModalComponent } from './modal/modal.component';

import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { BookingService } from './#services/booking/booking.service';
import { ErrorService } from './#services/Error/error.service';
import { AuthService } from './#services/auth/auth.service';
import { UserService } from './#services/user/user.service';
import { ReservationService } from './#services/reservation/reservation.service';
import { AttendanceService } from './#services/attendance/attendance.service';
import { ModalService } from './#services/modal/modal.service';
import { RoleGuardService } from './_guard/role-guard/role-guard.service';
import { JwtHelperService } from '@auth0/angular-jwt';

export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MenubarComponent,
    StartComponent,
    CalendarComponent,
    EventsComponent,
    BookingComponent,
    ModalComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:4200']
      }
    }),
  ],
  providers: [
    AuthService,
    ErrorService,
    BookingService,
    UserService,
    ReservationService,
    AttendanceService,
    ModalService,
    RoleGuardService,
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
