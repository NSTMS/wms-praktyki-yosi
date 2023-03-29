import { Injectable } from '@angular/core';
import { ErrorService } from '../error-handling/error.service';
import { magazineToAdd, magazineToEdit } from '@static/types/magazineTypes';
import { HttpClient } from '@angular/common/http';
import { Observable, map, catchError, tap, throwError } from 'rxjs';
import { product } from '@static/types/productTypes';

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

  constructor(private _errorService: ErrorService, private http: HttpClient) {}

  GetAll(): Observable<any> {
    return this.http.get(this.link).pipe(
      map((magazines) => {
        return magazines;
      })
    );
  }

  GetById(id: number): Observable<any> {
    return this.http.get(this.link + '/' + id).pipe(
      map((user) => {
        return user;
      })
    );
  }

  GetAllProducts(id: number): Observable<any> {
    return this.http.get(`${this.link}/${id}/products`).pipe(
      map((data) => {
        return data as product[];
      }),
      catchError((error: any) => {
        this._errorService.HandleBadResponse(error);
        throw error;
      })
    );
  }

  GetLocations(productId: number, magazineId: number): Observable<any> {
    return this.http
      .get(`${this.link}/${magazineId}/products/${productId}`)
      .pipe(
        map((data) => {
          return data as product[];
        })
      );
  }

  Add(mazgaine: magazineToAdd): Observable<any> {
    return this.http.post(this.link, JSON.stringify({ ...mazgaine })).pipe(
      tap(() => {
        return true;
      })
    );
  }

  Edit(id: number, newMagazine: magazineToEdit): Observable<any> {
    return this.http
      .put(this.link + '/' + id, JSON.stringify(newMagazine))
      .pipe(
        tap(() => {
          return true;
        })
      );
  }

  Delete(id: number): Observable<any> {
    return this.http.delete(this.link + '/' + id).pipe(
      tap(() => {
        return true;
      })
    );
  }
}
