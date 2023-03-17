import { Injectable } from '@angular/core';
declare var require: any;
const connection = require("src/static/connection.json")
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`
  async logIn(email: string, password: string): Promise<any> {
    return fetch(this.link + "/login", {
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      method: "POST",
      body: JSON.stringify({ "email": email, "password": password })
    })
    .then(r => r.json())
.then(res => {
      return res;
    })
    .catch(ex => {
      return null;
    });
  }
  
  async registerUser(email: string, password: string, confirmPassword: string) {
    try {
      const response = await fetch(this.link+"/register", {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify({ "email": email, "password": password, "confirmPassword": confirmPassword })
      });
      return response;
    } catch (ex: unknown) {
      return null;
    }
  }

}
