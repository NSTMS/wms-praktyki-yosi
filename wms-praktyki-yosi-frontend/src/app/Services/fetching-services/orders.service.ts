import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { orderToSend,updatedOrder,orderItem, orderSendItem } from '@static/types/orderTypes';

declare var require: any;
const connection = require('@static/connection.json');

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/orders`;
  listColumns : string[] = ['id','client','magazineId','interval','status','message','info','edit','delete']
  infoColumns : string[] = ['id','productId', 'arriving','quantity','tag','edit','delete']
  constructor(private http: HttpClient) { }

  async GetAll(term: string) {
    return await firstValueFrom(this.http.get(this.link + "?searchTerm=" + term));
  }
  async GetById(guid: string) {
    return await firstValueFrom(this.http.get(this.link + '/' + guid))
  }
  async AddNewOrder(order: orderToSend){
    return await firstValueFrom(this.http.post(this.link , order))
  }
  async UpdateOrder(guid: string, updatedOrder :updatedOrder ){
    return await firstValueFrom(this.http.put(this.link + "/" + guid, updatedOrder))
  }
  async DeleteOrder(guid: string)
  {
    return await firstValueFrom(this.http.delete(this.link  + "/" + guid))
  }
  async AddProductToOrder(guid : string, newProd : orderSendItem)
  {
    return await firstValueFrom(this.http.post(this.link  + "/" + guid + "/items/", newProd))
  }

  async UpdateProductInOrder(guid : string, prodId : string, newProd : orderItem){
    return await firstValueFrom(this.http.put(this.link  + "/" + guid + "/items/" + prodId , newProd))
  }
  async DeleteProductFromOrder(guid : string, prodId : string){
    return await firstValueFrom(this.http.delete(this.link  + "/" + guid + "/items/" + prodId))

  }
  // edit + delete = deit

}
