import { Injectable } from '@angular/core';
import { user } from '../types/userTypes';
declare var require: any;
const connection = require("src/static/connection.json")
@Injectable({
  providedIn: 'root'
})
export class AdminPanelService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`
  columns = ["Id", "Email", "PasswordHash", "Role"]

  constructor() { }

  async GetAll() {    
    try {
      const response = await fetch(this.link+"/users", {
        headers: {
          Authorization : `Bearer ${localStorage.getItem("token")}`
        }
      });
      const users = await response.json();
      return users;
    }
    catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }
  }
  async GetInfoFromToken() {    
    try {
      const response = await fetch(this.link+"/info", {
        headers: {
          Authorization : `Bearer ${localStorage.getItem("token")}`
        }
      });
      const data = await response.json();
      return data;
    }
    catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }
  }
  async GetById(id: number) {
    try {
      const response = await fetch(this.link + "/users/" + id, {
        headers: {
          Authorization : `Bearer ${localStorage.getItem("token")}`
        }
      })
      const user = await response.json();
      return user;
    }
    catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }
  }

  async Put(id: number, updatedUser: user) {
    try {
      const response = await fetch(this.link + "/users/" + id,
        {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem("token")}`
          },
          method: "PUT",
          body: JSON.stringify(updatedUser)
        });
      const returnedUser = await response.json();
      return returnedUser;
    } catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }

  }
  async Delete(id: number) {
    const response = await fetch(this.link + "/users" + id,
      {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
          Authorization : `Bearer ${localStorage.getItem("token")}`
        },
        method: "DELETE"
      });
    const user = await response.json();
    return user;
  }
}
