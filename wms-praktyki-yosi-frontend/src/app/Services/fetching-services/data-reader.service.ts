import { Injectable } from '@angular/core';
import type { product, productToAdd } from '@static/types/productTypes';
import { ErrorService } from '@services/error-handling/error.service';
import { HttpClient } from '@angular/common/http';
import { Observable, map, catchError, tap, throwError } from 'rxjs';
declare var require: any;
const connection = require('static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class DataReaderService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/products`;
  columns = ['id', 'productName', 'ean', 'price', 'quantity'];

  constructor(private http: HttpClient, private _errorHandler: ErrorService) {}

  GetAll(): Observable<any> {
    return this.http.get(this.link).pipe(
      map((data) => {
        return data;
      })
    );
  }

  GetById(id: number): Observable<any> {
    return this.http.get(this.link + '/' + id).pipe(
      map((data) => {
        return data;
      }),
      catchError((error) => {
        console.error(error);
        throw error[0];
      })
    );
  }

  Put(id: number, newProduct: productToAdd): Observable<any> {
    return this.http
      .put(`${this.link}/${id}`, { ...newProduct, ...{ locations: [] } })
      .pipe(
        map((data) => {
          return data;
        }),
        catchError((error: any) => {
          this._errorHandler.errorMessageShow(error);
          throw error;
        })
      );
  }

  Post(newProduct: productToAdd): Observable<any> {
    return this.http
      .post(this.link, { ...newProduct, ...{ locations: [] } })
      .pipe(
        map((data) => {
          return data;
        }),
        catchError((error: any) => {
          this._errorHandler.errorMessageShow(error);
          throw error;
        })
      );
  }

  Delete(id: number): Observable<any> {
    return this.http.delete(this.link + '/' + id).pipe(
      map((data) => {
        return data;
      }),
      catchError((error) => {
        console.error(error);
        throw error[0];
      })
    );
  }
}
