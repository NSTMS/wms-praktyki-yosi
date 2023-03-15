import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataReaderService } from '../data-reader.service';
import type { product, productToAdd } from '../types/productTypes';
import {FormsModule} from "@angular/forms"

@Component({
  selector: 'app-edit-form',
  templateUrl: './edit-form.component.html',
  styleUrls: ['./edit-form.component.scss']
})
export class EditFormComponent {
  id: number;
  // prod : product;
  name: string = ""
  ean = ""
  quantity = 0
  price = 0.0

  constructor(private route: ActivatedRoute, private _reader: DataReaderService) {
    this.id = this.route.snapshot.params["id"];
    // _reader.GetById(this.id).then(res => {
    //   this.prod = res as product
    //   if (!this.prod) return;
    //   this.name = this.prod.productName
    //   this.ean = this.prod.EAN
    //   this.quantity = this.prod.quantity
    //   this.price = this.prod.price
    // })
   

  }

  handleSubmit(){
    this._reader.Put(this.id ,{
      productName: this.name,
      EAN: this.ean,
      price: this.price,
      quantity: this.quantity
    } as productToAdd)

    console.log(this._reader.GetAll())
  }


}
