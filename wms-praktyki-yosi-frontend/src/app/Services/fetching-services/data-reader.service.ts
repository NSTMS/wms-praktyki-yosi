import { Injectable } from '@angular/core';
import type { productToAdd } from '@static/types/productTypes';
import { HttpClient } from '@angular/common/http';
import { Observable, map, catchError, tap, throwError, firstValueFrom } from 'rxjs';
declare var require: any;
const connection = require('static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class DataReaderService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/products`;
  columns = ['id', 'productName', 'ean', 'price', 'quantity'];

  constructor(private http: HttpClient) { }

  async GetAll() {
    return await firstValueFrom(this.http.get(this.link))
  }

  async GetById(id: number) {
    return await firstValueFrom(this.http.get(this.link + '/' + id))
  }
  async Put(id: number, newProduct: productToAdd) {
    return await firstValueFrom(this.http
      .put(`${this.link}/${id}`, { ...newProduct, ...{ locations: [] } })
    )
  }

  async Post(newProduct: productToAdd) {
    return await firstValueFrom(this.http
      .post(this.link, { ...newProduct, ...{ locations: [] } }))
  }

  async Delete(id: number) {
    return await firstValueFrom(this.http.delete(this.link + '/' + id))
  }
}
