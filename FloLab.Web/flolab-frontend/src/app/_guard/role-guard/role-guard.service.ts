import { Injectable } from '@angular/core';
import { Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../../_services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService {

  constructor(
    public auth: AuthService,
    public router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data.expectedRole;
    const tokenRole = this.auth.getRole();

    if (!this.auth.isAuthenticated() || tokenRole !== expectedRole) {
      return false;
    }
    return true;
  }
}
