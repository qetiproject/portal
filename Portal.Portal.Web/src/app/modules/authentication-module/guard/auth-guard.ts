import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from 'src/app/shared/infrastructure/CustomApi/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {

  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const user = JSON.parse(localStorage.getItem('user'));

    if (user?.token) {
      const originalDate = new Date();
      // Extract components
      const year = originalDate.getFullYear();
      const month = String(originalDate.getMonth() + 1).padStart(2, '0');
      const day = String(originalDate.getDate()).padStart(2, '0');
      const hours = String(originalDate.getHours()).padStart(2, '0');
      const minutes = String(originalDate.getMinutes()).padStart(2, '0');
      const seconds = String(originalDate.getSeconds()).padStart(2, '0');
      // Format new date string
      const formattedDate = `${year}-${month}-${day}T${hours}:${minutes}:${seconds}Z`;

      if(user?.expiration < formattedDate) {
        this.authService.logout();
        return false;
      } else {
        return true;
      }
    }


    this.router.navigateByUrl('/login');
    return false;
  }



}