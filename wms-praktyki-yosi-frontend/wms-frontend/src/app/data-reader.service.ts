import { Injectable } from '@angular/core';
import { type product, type productToAdd } from './types/productTypes';
@Injectable({
  providedIn: 'root'
})
export class DataReaderService {
  link : string = 'http://189.91.31.162:5000/api/products'
  columns = ["id", "productName", "ean", "price", "quantity"]
  // constructor() {}


  async GetAll() {
    const response = await fetch(this.link);
    const products = await response.json();
    console.log(`products`, products);
    return products;
  }

  async GetById(id: number){
    const response = await fetch(this.link+"/"+id);
    const product = await response.json();
    return product;
  }

  async Put(id: number, newProduct:productToAdd) {
    const response = await fetch(this.link+"/"+id,
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
  }

  async Post(newProduct: productToAdd){
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
  }

  async Delete(id: number){
    const response = await fetch(this.link+"/"+id,
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
