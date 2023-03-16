import { Injectable } from '@angular/core';
import type { product, productToAdd } from '../types/productTypes';
declare var require: any;
const connection = require("src/static/connection.json")

@Injectable({
  providedIn: 'root'
})
export class DataReaderService {


  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/products`
  columns = ["id", "productName", "ean", "price", "quantity"]
  // constructor() {}


  async GetAll() {
    try {
      const response = await fetch(this.link);
      const products = await response.json();
      return products;
    }
    catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }

  }

  async GetById(id: number) {
    try {
      const response = await fetch(this.link + "/" + id)
      const product = await response.json();
      return product;
    }
    catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }
  }

  async Put(id: number, newProduct: productToAdd) {
    try {
      const response = await fetch(this.link + "/" + id,
        {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          method: "PUT",
          body: JSON.stringify(newProduct)
        });
      const product = await response.json();
      return product;
    } catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }

  }

  async Post(newProduct: productToAdd) {
    try {
      const response = await fetch(this.link,
        {
          headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
          },
          method: "POST",
          body: JSON.stringify(newProduct)
        });
      const product = await response.json();
      return product;
    } catch (ex: unknown) {
      console.log(JSON.stringify(ex))
    }
  }

  async Delete(id: number) {
    const response = await fetch(this.link + "/" + id,
      {
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        },
        method: "DELETE"
      });
    const product = await response.json();
    return product;
  }

}
