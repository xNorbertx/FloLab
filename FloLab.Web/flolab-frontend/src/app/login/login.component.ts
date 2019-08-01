import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth/auth.service';
import { AttendanceService } from '../_services/attendance/attendance.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private route: Router,
    private auth: AuthService,
    private attendanceService: AttendanceService
  ) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login() {
    if (this.loginForm.invalid) {
      return;
    }

    this.auth.login(this.loginForm.value).subscribe(
      () => {
        const role = this.auth.getRole();
        if (role === "User") {
          this.route.navigate(['/start']);
        } else if (role === "Admin") {
          this.route.navigate(['/admin']);
        } else {
          //Todo: don't leave this as an alert message
          alert('Not allowed to log in');
        }
      },
      error => {
        //Todo: proper error management
        console.log(error);
      }
    )
  }

  //Todo: REMOVE WHEN DONE WITH DEVELOPMENT
    prefillUser(): void {
        this.loginForm.setValue({
            email: 'norbert.g.e.bakker@gmail.com',
            password: 'Telefoon06!'
        });
    }

    prefillAdmin(): void {
        this.loginForm.setValue({
            email: 'admin@flolab.nl',
            password: 'LiveLoveLaugh123!'
        });
    }
}
