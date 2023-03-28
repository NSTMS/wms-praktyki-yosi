import { Injectable } from '@angular/core';
declare var require: any;
const connection = require('static/connection.json');
import { ErrorService } from '@services/error-handling/error.service';
import { HttpClient } from '@angular/common/http';
import { Observable,map,catchError,tap,throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class DocumentsService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/documents`;
  columns = ['guid', 'magazineId','client','date','totalQuantity','realizeQuantity','finished'  ];
  constructor(private _errorService: ErrorService, private http: HttpClient,private _errorHandler : ErrorService) {}

  GetAll(): Observable<any> {
    return this.http.get(this.link).pipe(
      map((data) =>{
        return data
      }),
      catchError((error)=>{
        this._errorHandler.HandleBadResponse(error);
        throw error;
      })
    );
  }
  GetById(id: string) : Observable<any> {
    return this.http.get(this.link + '/' + id).pipe(
      map((data) => {
        return data
      }),
      catchError((error) => {
        console.error(error);
        throw error[0];
      })
    );
  }
  MarkAsFinished(guid : string, finished: boolean): Observable<any> {
    console.log("elo");
    
    return this.http.post(this.link + '/' + guid + "/markasfinished", finished).pipe(
      map((res)=>{
        console.log(res);
        
        return res;
      })
    )
  }
  Delete(guid : string): Observable<any> {
    return this.http.get(this.link + "/" + guid).pipe();
}

}
