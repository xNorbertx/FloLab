import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AdminComponent } from './admin/admin.component';
import { StartComponent } from './start/start.component';
import { CalendarComponent } from './calendar/calendar.component';
import { EventsComponent } from './events/events.component';
import { BookingComponent } from './booking/booking.component';
import { NewUserComponent } from './new-user/new-user.component';
import { RoleGuardService as RoleGuard } from './_guard/role-guard/role-guard.service'; 

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'admin', component: AdminComponent, canActivate: [RoleGuard], data: { expectedRole: 'Admin' }, pathMatch: 'full' },
    { path: 'admin/new-user', component: NewUserComponent, canActivate: [RoleGuard], data: { expectedRole: 'Admin' } },
    { path: 'start', component: StartComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
    { path: 'calendar', component: CalendarComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
    { path: 'events', component: EventsComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
    { path: 'bookings', component: BookingComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
    { path: '*', redirectTo: 'login' }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
