import { Injectable } from '@angular/core';
import { locationToEdit, locationToAdd } from '@static/types/locationTypes';
import { ErrorService } from '@services/error-handling/error.service';

declare var require: any;
const connection = require("@static/connection.json")

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  columns = ["id", "magazineId", "position", "quantity"]

  headers = {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    Authorization : `Bearer ${localStorage.getItem("token")}`
  }

  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/locations`
  constructor(private _errorHandler: ErrorService ) { }

  async EditLocation (id: number, newLocation: locationToEdit){
    const response = await fetch(`${this.link}/${id}`, {
      method: "PUT",
      headers: this.headers,
      body: JSON.stringify({
        ...newLocation
      })
    })

    if (response.ok)
      return true;

    const json = await response.json()

    json?.Errors.forEach((errCode: number) => {
      this._errorHandler.handleErrorCode(errCode);
    })
    return false;
  }

  async AddLocation(newLocation: locationToAdd){
    const response = await fetch(this.link, {
      method: "POST",
      headers: this.headers,
      body: JSON.stringify({
        ...newLocation
      })
    })

    if (response.ok)
      return true;

    const json = await response.json()

    json?.Errors.forEach((errCode: number) => {
      this._errorHandler.handleErrorCode(errCode);
    })

    return false;
  }

  async GetById(id: number) {
    const response = await fetch(this.link + "/" + id, {
      headers: this.headers
    })

    if (response.ok)
      return await response.json();

    const json = await response.json()

    json?.errors.forEach((errCode: number) => {
      this._errorHandler.handleErrorCode(errCode)
    })
  }

  async Delete(id: number){
    const response = await fetch(this.link + "/" + id, {
      headers: this.headers,
      method: "DELETE"
    })

    if (response.ok)
      return true;

    const json = await response.json()

    json?.Errors.forEach((errCode: number) => {
      this._errorHandler.handleErrorCode(errCode);
    })
    return false;
  }



}
