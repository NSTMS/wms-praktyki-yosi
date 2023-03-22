import { Injectable } from '@angular/core';
import { ErrorService } from '../error-handling/error.service';
import { magazineToAdd } from '@static/types/magazineTypes';

declare var require: any;
const connection = require('@static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class MagazineService {
  columns = ['id', 'name', 'address'];

  headers = {
    Accept: 'application/json',
    'Content-Type': 'application/json',
    Authorization: `Bearer ${localStorage.getItem('token')}`,
  };

  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/magazines`;

  constructor(private _errorHandler: ErrorService) {}

  async GetAll() {
    const response = await fetch(this.link, {
      headers: this.headers,
    });

    if (response.ok) return await response.json();

    this._errorHandler.HandleBadResponse(response);
  }

  async Add(newMagazine: magazineToAdd) {
    const response = await fetch(this.link, {
      method: 'POST',
      headers: this.headers,
      body: JSON.stringify({
        ...newMagazine,
      }),
    });

    if (response.ok) return true;

    this._errorHandler.HandleBadResponse(response);

    return false;
  }

  async GetById(id: number) {
    const response = await fetch(`${this.link}/${id}`, {
      headers: this.headers,
    });

    if (response.ok) return await response.json();

    this._errorHandler.HandleBadResponse(response);
  }

  async Edit(id: number, newMagazine: magazineToAdd) {
    const response = await fetch(`${this.link}/${id}`, {
      method: 'PUT',
      headers: this.headers,
      body: JSON.stringify({
        ...newMagazine,
      }),
    });

    if (response.ok) return true;

    this._errorHandler.HandleBadResponse(response);

    return false;
  }

  async Delete(id: number) {
    const response = await fetch(this.link + '/' + id, {
      headers: this.headers,
      method: 'DELETE',
    });

    if (response.ok) return true;

    this._errorHandler.HandleBadResponse(response);

    return false;
  }

  async GetAllProducts(id: number) {
    const response = await fetch(`${this.link}/${id}/products`, {
      headers: this.headers,
    });

    if (response.ok) return await response.json();

    this._errorHandler.HandleBadResponse(response);
  }

  async GetLocations(productId: number, magazineId:number){
    const response = await fetch(`${this.link}/${magazineId}/products/${productId}`, {
      headers: this.headers,
    });

    if (response.ok) return await response.json();

    this._errorHandler.HandleBadResponse(response);
  }
}
