import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError,retry} from 'rxjs';
// declare var require: any;
// const connection = require('static/connection.json');


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem("token");        
    request = request.clone({
      setHeaders: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`
      }
    });
    return next.handle(request)
    .pipe(
      catchError((error : HttpErrorResponse)=>{
        return throwError(() =>{
          return error.error.errors
        })
      }));
  }
}
