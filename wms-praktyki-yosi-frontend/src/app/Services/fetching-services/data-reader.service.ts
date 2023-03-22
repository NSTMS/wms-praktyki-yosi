import { Injectable } from '@angular/core';
import type { product, productToAdd } from '@static/types/productTypes';
import { ErrorService } from '@services/error-handling/error.service';
declare var require: any;
const connection = require('src/static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class DataReaderService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/products`;
  columns = ['id', 'productName', 'ean', 'price', 'quantity'];

  constructor(private _errorService: ErrorService) {}

  async GetAll() {
    try {
      const response = await fetch(this.link, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      if (response.ok) {
        const products = await response.json();
        return products;
      } else {
        return null;
      }
    } catch (ex: unknown) {
      console.error(ex);
    }
  }

  async GetById(id: number) {
    try {
      const response = await fetch(this.link + '/' + id, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });

      const product = await response.json();

      if (response.ok) {
        return product;
      }

      return product.Errors[0];
    } catch (ex: unknown) {
      console.error(ex);
    }
  }

  async Put(id: number, newProduct: productToAdd) {
    try {
      const response = await fetch(this.link + '/' + id, {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
        method: 'PUT',
        body: JSON.stringify(newProduct),
      });
      if (response.ok) return true;

      const json = await response.json();
      json?.Errors.forEach((errCode: number) => {
        this._errorService.handleErrorCode(errCode);
      });
      return false;
    } catch (ex: unknown) {
      console.error(ex);
      return false;
    }
  }

  async Post(newProduct: productToAdd) {
    try {
      const response = await fetch(this.link, {
        headers: {
          Accept: 'application/json',
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
        method: 'POST',
        body: JSON.stringify(newProduct),
      });
      const product = await response.json();
      return product;
    } catch (ex: unknown) {
      console.error(ex);
    }
  }

  async Delete(id: number) {
    const response = await fetch(this.link + '/' + id, {
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        Authorization: `Bearer ${localStorage.getItem('token')}`,
      },
      method: 'DELETE',
    });
    const product = await response.json();
    return product;
  }

}
