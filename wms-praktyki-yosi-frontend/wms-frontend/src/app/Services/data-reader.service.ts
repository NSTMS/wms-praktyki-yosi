import { Injectable } from '@angular/core';
import type { product, productToAdd } from '@static/types/productTypes';
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
      const response = await fetch(this.link, {
        headers: {
          Authorization : `Bearer ${localStorage.getItem("token")}`
        }
      });
      console.log(response);
      if(response.ok)
      {
        const products = await response.json();
        return products;
      }
      else{
        return null
      }

    }
    catch (ex: unknown) {
      console.log(ex);
      }
  }

  async GetById(id: number) {
    try {
      const response = await fetch(this.link + "/" + id, {
        headers: {
          Authorization : `Bearer ${localStorage.getItem("token")}`
        }
      })
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
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem("token")}`
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
            'Content-Type': 'application/json',
            Authorization : `Bearer ${localStorage.getItem("token")}`

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
          'Content-Type': 'application/json',
          Authorization : `Bearer ${localStorage.getItem("token")}`
        },
        method: "DELETE"
      });
    const product = await response.json();
    return product;
  }

}
