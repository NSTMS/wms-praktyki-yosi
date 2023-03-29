import { Injectable } from '@angular/core';
declare var require: any;
const connection = require('static/connection.json');
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { EditDialogData } from '@static/types/documentTypes';
@Injectable({
  providedIn: 'root',
})
export class DocumentsService {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/documents`;
  columns = [
    'guid',
    'magazineId',
    'client',
    'date',
    'totalQuantity',
    'realizeQuantity',
    'finished',
  ];
  constructor(private http: HttpClient) {}

  async GetAll() {
    return await firstValueFrom(this.http.get(this.link));
  }
  async GetById(id: string) {
    return await firstValueFrom(this.http.get(this.link + '/' + id));
  }
  async MarkAsFinished(guid: string, finished: boolean) {
    return await firstValueFrom(
      this.http.post(this.link + '/' + guid + '/markasfinished', finished)
    );
  }
  async VisitLocation(guid: string,){
    return;
  }
  async Delete(guid: string) {
    return await firstValueFrom(this.http.get(this.link + '/' + guid));
  }
  UpdateItem(id: string, prodId: number, item : EditDialogData){
    return this.http.put(this.link + '/' + id + "/" + prodId,item); 
  }
}
