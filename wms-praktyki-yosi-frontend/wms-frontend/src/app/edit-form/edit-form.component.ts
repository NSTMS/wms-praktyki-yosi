import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataReaderService } from '../data-reader.service';
import type { product, productToAdd } from '../types/productTypes';
// import { ReactiveFormsModule } from "@angular/forms"
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-edit-form',
  templateUrl: './edit-form.component.html',
  styleUrls: ['./edit-form.component.scss']
})
export class EditFormComponent {
  id: number;

  name = new FormControl("")
  ean = new FormControl("")
  quantity = new FormControl(0)
  price = new FormControl(0.0)

  constructor(private route: ActivatedRoute, private _reader: DataReaderService) {
    this.id = this.route.snapshot.params["id"];
    _reader.GetById(this.id).then(res => {
         const prod = res as product
         this.name.setValue(prod.productName)
         this.ean.setValue(prod.EAN)
         this.quantity.setValue(prod.quantity)
         this.price.setValue(prod.price)
        })


  }

  handleSubmit(){
    this._reader.Put(this.id ,{
      productName: this.name.value,
      EAN: this.ean.value,
      price: this.price.value,
      quantity: this.quantity.value
    } as productToAdd)

    console.log(this._reader.GetAll())
  }


}
