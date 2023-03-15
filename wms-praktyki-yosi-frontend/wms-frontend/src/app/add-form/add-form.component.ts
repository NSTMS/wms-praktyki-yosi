import { Component } from '@angular/core';
import { DataReaderService } from '../data-reader.service';
import type { productToAdd } from '../types/productTypes';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss']
})
export class AddFormComponent {
  name = new FormControl("")
  ean = new FormControl("")
  quantity = new FormControl(0)
  price = new FormControl(0.0)

  constructor( private _reader: DataReaderService) {

  }

  handleSubmit(){
    this._reader.Post({
      productName: this.name.value,
      ean: this.ean.value,
      price: this.price.value,
      quantity: this.quantity.value
    } as productToAdd)

    console.log(this._reader.GetAll())
  }



}
