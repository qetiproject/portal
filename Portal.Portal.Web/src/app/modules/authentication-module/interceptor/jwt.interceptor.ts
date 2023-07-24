import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { LanguageModel } from 'src/app/shared/infrastructure/Model/languageModel';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const user = JSON.parse(localStorage.getItem('user'));

    const defaultLang = LanguageModel.Ka;
    let activeLanguage = localStorage.getItem('activeLanguage');

    if(user?.token) {
      const clonedReq = request.clone({
        headers: new HttpHeaders({
          'Authorization': 'Bearer ' + user.token,
          'Accept-Language': activeLanguage || defaultLang
        })
      });

      return next.handle(clonedReq);
    } else {
      return next.handle(request);
    }
  }
}
