import { Component } from '@angular/core';
import { DataReaderService } from '../data-reader.service';
import type { productToAdd } from '../types/productTypes';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})
export class AddFormComponent {
  name: string = ""
  ean = ""
  quantity = ""
  price = ""

  constructor( private _reader: DataReaderService) {

  }

  handleSubmit(){
    this._reader.Post({
      productName: this.name,
      EAN: this.ean,
      price: Number(this.price),
      quantity: parseInt( this.quantity)
    } as productToAdd)

    console.log(this._reader.GetAll())
  }



}
