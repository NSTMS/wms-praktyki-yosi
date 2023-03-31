import { Injectable } from '@angular/core';
import { locationToEdit, locationToAdd } from '@static/types/locationTypes';
import { ErrorService } from '@services/error-handling/error.service';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

declare var require: any;
const connection = require('@static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  columns = ['id', 'magazineId', 'position', 'quantity'];

  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/locations`;
  constructor(private _errorHandler: ErrorService, private http: HttpClient) {}

  async EditLocation(id: number, newLocation: locationToEdit) {
    return await firstValueFrom(this.http.put(`${this.link}/${id}`, newLocation))
  }

  async AddLocation(newLocation: locationToAdd) {
    return await firstValueFrom(this.http.post(this.link, {...newLocation,}))
  }

  async GetById(id: number) {
    return await firstValueFrom(this.http.get(this.link + '/' + id))
  }
  async Delete(id: number){
    return await firstValueFrom(this.http.delete(this.link + '/' + id))
  }
}
