import { Injectable } from '@angular/core';
declare var require: any;
const connection = require('@static/connection.json');
@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`;

  async logIn(email: string, password: string): Promise<any> {
    const response = await fetch(this.link + '/login', {
      method: 'POST',
      headers:{
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email: email, password: password }),
    });
    if (response.ok) {
      const res = await response.json();
      return res;
    } else {
      return null;
    }
  }

  async registerUser(email: string, password: string, confirmPassword: string) {
    try {
      const response = await fetch(this.link + '/register', {
        method: 'POST',
        headers:{
          Accept: 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: email,
          password: password,
          confirmPassword: confirmPassword,
        }),
      });
      return response;
    } catch (ex: unknown) {
      return null;
    }
  }
}
