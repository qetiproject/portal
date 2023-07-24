import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { LoginResponse } from '../PortalHttpClient';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn = false;
  isLoggedInByButton = false;
  isLoggedOutByButton = false;

  constructor(private router: Router) {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user?.token) {
      this.isLoggedIn = true;
    }
  }

  get isAutorized(): boolean {
    return this.isLoggedIn;
  }
  get isAutorizedByButtonClickEvent(): boolean {
    return this.isLoggedInByButton;
  }

  get isLoggedOutByButtonClickEvent(): boolean {
    return this.isLoggedOutByButton;
  }

  login(value: LoginResponse) {
    localStorage.setItem('user', JSON.stringify(value));
    this.isLoggedIn = true;
    this.isLoggedInByButton = true;
    this.router.navigate([`/management/employee-service/employee-profile/${value.employeeId}`])
  }

  logout() {
    localStorage.removeItem('user');
    localStorage.removeItem('tabSelectEventTitle');
    this.isLoggedIn = false;
    this.isLoggedOutByButton = true;
    this.router.navigate([`login`]);
  }
  
}