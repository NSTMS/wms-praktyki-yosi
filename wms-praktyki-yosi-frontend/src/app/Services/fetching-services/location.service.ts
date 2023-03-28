import { Injectable } from '@angular/core';
import { locationToEdit, locationToAdd } from '@static/types/locationTypes';
import { ErrorService } from '@services/error-handling/error.service';
import { HttpClient } from '@angular/common/http';
import { catchError,map,Observable,tap } from 'rxjs';

declare var require: any;
const connection = require('@static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  columns = ['id', 'magazineId', 'position', 'quantity'];

  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/locations`;
  constructor(private _errorHandler: ErrorService, private http: HttpClient) {}

  EditLocation(id: number, newLocation: locationToEdit): Observable<any>  {
    return this.http.put(`${this.link}/${id}`, newLocation).pipe(
      map((data)=>{
        return data        
      }),
      catchError((error:any)=>{
        this._errorHandler.errorMessageShow(error)
        throw false;
      })
    )
  }

  AddLocation(newLocation: locationToAdd): Observable<any> {
    return this.http.post(this.link, {
     ...newLocation
    }).pipe(
      tap(() => {
        return true;
      }),
      catchError((error) => {
        error.forEach((errCode: number) => {
          this._errorHandler.handleErrorCode(errCode);
        });
        throw (error)
      })
    )
  }

  GetById(id: number): Observable<any> {
    return this.http.get(this.link + '/' + id).pipe(
      map((data) => {
        return data
      }),
      catchError((error) => {
        throw this._errorHandler.handleErrorCode(error);
      })
    );
  }
  Delete(id: number): Observable<any>  {
    return this.http.delete(this.link + '/' + id).pipe(
      map((data) => {
        return true
      }),
      catchError((error) => {
        error.forEach((errCode: number) => {
          this._errorHandler.handleErrorCode(errCode);
        });
        throw (error)
      })
    );
  }
}
