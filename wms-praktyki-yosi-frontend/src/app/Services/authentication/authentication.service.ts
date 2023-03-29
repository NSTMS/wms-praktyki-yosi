import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
declare var require: any;
const connection = require('@static/connection.json');
import { firstValueFrom } from 'rxjs';
import { ErrorService } from '../error-handling/error.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  constructor(private _errorService: ErrorService, private http: HttpClient) {}
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`;

  async logIn(email: string, password: string) {
    return await firstValueFrom(
      this.http.post(this.link + '/login', { email: email, password: password })
    );
  }

  async registerUser(email: string, password: string, confirmPassword: string) {
    const regdata = {
      email: email,
      password: password,
      confirmPassword: confirmPassword,
    };
    return await firstValueFrom(
      this.http.post(this.link + '/register', regdata)
    );
  }
}
