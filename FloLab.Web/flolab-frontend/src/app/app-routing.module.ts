import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component'
import { StartComponent } from './start/start.component';
import { CalendarComponent } from './calendar/calendar.component';
import { EventsComponent } from './events/events.component';
import { RoleGuardService as RoleGuard } from './_guard/role-guard/role-guard.service'; 

//Todo: add roleguards and login-guard
const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'start', component: StartComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
  { path: 'calendar', component: CalendarComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
  { path: 'events', component: EventsComponent, canActivate: [RoleGuard], data: { expectedRole: 'User' } },
  { path: '*', redirectTo: 'login' }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
