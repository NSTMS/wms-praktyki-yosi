import { Injectable } from '@angular/core';
import { user } from '@static/types/userTypes';
import { Observable,map,catchError,tap,throwError } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ErrorService } from '../error-handling/error.service';

declare var require: any;
const connection = require('static/connection.json');
@Injectable({
  providedIn: 'root',
})
export class AdminPanelService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/account`;
  columns = ['Id', 'Email', 'PasswordHash', 'Role'];

  constructor( private _errorService: ErrorService,private http: HttpClient) {}
  GetAll(): Observable<any> {
    return this.http.get(this.link + "/users").pipe(
      map((users) =>{
        return users
      }),
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error;
      })
    );
  }
  GetInfoFromToken(): Observable<any> {
    return this.http.get(this.link + '/info').pipe(
      map((users) =>{
        return users
      }),
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error;
      })
    );
  }
   GetById(id: number): Observable<any> {
    return this.http.get(this.link + '/users/' + id).pipe(
      map((user) =>{
        return user
      }),
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error;
      })
    );
  }

  Put(id: number, updatedUser: user): Observable<any> {

    return this.http.put(this.link + '/users/' + id,JSON.stringify(updatedUser)).pipe(
      map((returnedUser) =>{
        return returnedUser
      }),
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error;
      })
    );
  }
  Delete(id: number): Observable<any> {
    return this.http.delete(this.link + '/users/' + id).pipe(
      catchError((error)=>{
        this._errorService.HandleBadResponse(error);
        throw error;
      })
    );
    }

}
