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

  constructor(private http: HttpClient) { }

  async GetAll(term: string) {
    return await firstValueFrom(this.http.get(this.link + "?searchTerm=" + term));
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
  async showShelves(id : number)
  {
    return await firstValueFrom(this.http.get(this.link + "/" + id +"/shelves"))
  }
  async GetShelfDetails(id : number, position : string)
  {
    return await firstValueFrom(this.http.get(this.link + "/" + id +"/shelves/" + position))
  }
  async ChnageShelfProductLocation(id : number, position : string,destination : string)
  {
    return await firstValueFrom(this.http.post(this.link + "/" + id +"/shelves/" + position + '/move', JSON.stringify( destination)))
  }
}
