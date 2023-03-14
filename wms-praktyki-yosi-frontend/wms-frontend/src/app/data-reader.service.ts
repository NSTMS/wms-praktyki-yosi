import { Injectable } from '@angular/core';
import { type product, type productToAdd } from './types/productTypes';
@Injectable({
  providedIn: 'root'
})
export class DataReaderService {

  private readonly data: product[]
  private idCounter: number;
  columns = ["id", "productName", "EAN", "price", "quantity"]
  constructor() {
    this.data = [
      {
        id: 0,
        productName: "Apples",
        EAN: "3748567323872",
        price: 12,
        quantity: 2879
      },
      {
        id: 1,
        productName: "Pears",
        EAN: "1874398798572",
        price: 6.80,
        quantity: 1783
      },
      {
        id: 2,
        productName: "Oranges",
        EAN: "98137598757932",
        price: 7.20,
        quantity: 3012
      }
    ];

    this.idCounter = this.data.length;

  }


  GetAll() {
    return this.data;
  }

  GetById(id: number){
    return this.data.find(p => p.id == id)
  }

  Put(id: number, newProduct:productToAdd) {
    const element = this.data.find(p => p.id == id)
    if (!element) return false
    Object.keys(newProduct as object).forEach(key => {
      (element as any)[key] = (newProduct as any)[key]
    })
    console.log(newProduct)
    return true;
  }

  Post(newProduct: productToAdd){
    try {
      const newElement = {
        id: this.idCounter++,
        ...newProduct
      } as product;
      this.data.push(newElement);
      return newElement.id;
    }
    catch{
      return -1
    }
  }

  Delete(id: number){
    let idInArr = -1
    this.data.forEach((value, index) => {
      if (value.id == id) idInArr = index
    })
    if (idInArr == -1) return false
    this.data.splice(idInArr, 1)
    return true
  }

}
