import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
declare var require: any;
const connection = require('@static/connection.json');
import { Observable, map, catchError, tap, throwError } from 'rxjs';
import { ErrorService } from '../error-handling/error.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private _errorService: ErrorService, private http: HttpClient) { }
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`;

   logIn(email: string, password: string): Observable<any> {
    return this.http.post(this.link + "/login", JSON.stringify({ email: email, password: password })).pipe(
      map((data)=>{
        return data
      }),
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error
      })
    )
  }

  registerUser(email: string, password: string, confirmPassword: string) {
    const regdata = JSON.stringify({
      email: email,
      password: password,
      confirmPassword: confirmPassword,
    })
    return this.http.post(this.link + "/register", regdata).pipe(
      map((data)=>{
        return data
      }),
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error
      })
    )
  }
}
