import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
declare var require: any;
const connection = require('static/connection.json');
@Injectable({
  providedIn: 'root',
})
export class AdminPanelService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`;
  columns = ['Id', 'Email', 'PasswordHash', 'Role'];

  constructor(private http: HttpClient) {}

  async GetAll(term : string) {
    return await firstValueFrom(this.http.get(this.link +  "/users?searchTerm=" + term));
}
  async GetInfoFromToken() {
    return await firstValueFrom(this.http.get(this.link + '/info'))
  }
  async GetById(id: string){
    return await firstValueFrom(this.http.get(this.link + '/users/' + id))
  }
  async Put(id: string, role: string){
    return await firstValueFrom(this.http.put(this.link + '/users/' + id, role))
  }
  async Delete(id: string){
    return await firstValueFrom(this.http.delete(this.link + '/users/' + id))
  }
}
