import { ThisReceiver } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { locationToEdit, locationToAdd } from '@static/types/locationTypes';
import { ErrorService } from '@services/error.service';

declare var require: any;
const connection = require("@static/connection.json")

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  columns = ["id", "position", "quantity"]

  headers = {
    'Accept': 'application/json',
    'Content-Type': 'application/json',
    Authorization : `Bearer ${localStorage.getItem("token")}`
  }

  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/locations`
  constructor(private _errorHandler: ErrorService ) { }

  async EditLocalization (id: number, newLocation: locationToEdit){
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

  async AddLocalization(newLocation: locationToAdd){
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

}
