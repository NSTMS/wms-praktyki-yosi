import { Injectable } from '@angular/core';
declare var require: any;
const connection = require("src/static/connection.json")
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/users`
  async signUp(email: string, password:string){
    try {
      const response = await fetch(this.link + "/login",
      {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        method: "POST",
        body: JSON.stringify({"email":email,"password":password})
      });
    const token = await response.json();
    return token;
    }
    catch (ex: unknown) {
      console.log(JSON.stringify(ex))
      return false;
    }

    // if(email == "test@test.com" && password == "password")
    // {
    //   return true;
    // }
    // else return false;
  }
}
