import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GuestGuard {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const user = JSON.parse(localStorage.getItem('user'));

    if (!user?.token) {
      // User is not authenticated, allow access to login page
      return true;
    }
    // User is authenticated, redirect to management page
    this.router.navigate(['/management']);
    return false;
  }
}
