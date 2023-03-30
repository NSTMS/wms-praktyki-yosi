import { Injectable } from '@angular/core';
import { magazineToAdd, magazineToEdit } from '@static/types/magazineTypes';
import { HttpClient } from '@angular/common/http';
import { product } from '@static/types/productTypes';
import { firstValueFrom } from 'rxjs';
declare var require: any;
const connection = require('@static/connection.json');

@Injectable({
  providedIn: 'root',
})
export class MagazineService {
  columns = ['id', 'name', 'address']
  link = `${connection.protocole}://${connection.ip}:${connection.port}/api/magazines`;

  constructor(private http: HttpClient) {}

  async GetAll(){
    return await firstValueFrom(this.http.get(this.link))
  }

  async GetById(id: number) {
    return await firstValueFrom(this.http.get(this.link + '/' + id))
  }

  async GetAllProducts(id: number) {
    return await firstValueFrom(this.http.get(`${this.link}/${id}/products`)) as product[]
  }

  async GetLocations(productId: number, magazineId: number) {
    return await firstValueFrom(this.http
      .get(`${this.link}/${magazineId}/products/${productId}`)) as product[]
  }

  async Add(magazine: magazineToAdd) {
    await firstValueFrom(this.http.post(this.link, magazine))
    console.log("elo",magazine)
    return true;
  }

  async Edit(id: number, newMagazine: magazineToEdit) {
    await firstValueFrom(this.http.put(this.link + '/' + id, newMagazine))
    return true;
  }

  async Delete(id: number) {
    await firstValueFrom(this.http.delete(this.link + '/' + id))
    return true;
  }
}
