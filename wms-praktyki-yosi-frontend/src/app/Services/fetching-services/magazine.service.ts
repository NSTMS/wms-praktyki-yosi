import { Injectable } from '@angular/core';
import { ErrorService } from '../error-handling/error.service';
import { magazineToAdd, magazineToEdit } from '@static/types/magazineTypes';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { product } from '@static/types/productTypes';
import { productLocation } from '@static/types/locationTypes';

declare var require: any;
const connection = require('@static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class MagazineService {
  columns = ['id', 'name', 'address'];

  // headers = {
  //   Accept: 'application/json',
  //   'Content-Type': 'application/json',
  //   Authorization: `Bearer ${localStorage.getItem('token')}`,
  // };

  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/magazines`;

  constructor(private _errorHandler: ErrorService, private http: HttpClient) {}

  async GetAll() {
    const response = await fetch(this.link);

    if (response.ok) return await response.json();

    this._errorHandler.HandleBadResponse(response);
  }

  async Add(newMagazine: magazineToAdd) {
    const response = await fetch(this.link, {
      method: 'POST',
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
    });

    if (response.ok) return await response.json();

    this._errorHandler.HandleBadResponse(response);
  }

  async Edit(id: number, newMagazine: magazineToEdit) {
    const response = await fetch(`${this.link}/${id}`, {
      method: 'PUT',
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
      method: 'DELETE',
    });

    if (response.ok) return true;

    this._errorHandler.HandleBadResponse(response);

    return false;
  }

  GetAllProducts(id: number) {
    return this.http.get(`${this.link}/${id}/products`).pipe(
      map((data) =>{
        return data as product[]
      }),
      catchError((error : any)=>{
        this._errorHandler.HandleBadResponse(error);
        throw error;
      })
    )
  }

  GetLocations(productId: number, magazineId: number) {
    return this.http.get(`${this.link}/${magazineId}/products/${productId}`).pipe(
      map((data) =>{
        return data as product[]
      }),
      catchError((error : any)=>{
        this._errorHandler.HandleBadResponse(error);
        throw error;
      })
    )
  }
}
