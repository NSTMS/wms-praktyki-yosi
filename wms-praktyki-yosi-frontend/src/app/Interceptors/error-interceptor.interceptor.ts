import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ErrorService } from '@app/Services/error-handling/error.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private _errorHandler : ErrorService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = localStorage.getItem("token");

    if (token) {
      request = request.clone({
        setHeaders: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: `Bearer ${token}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse)=>{
        this._errorHandler.errorMessageShow(error.error.errors)
        return throwError(() => new Error(error.error.errors))
      })
    );
  }
}
