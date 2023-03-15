import { Component } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { DataReaderService } from '../data-reader.service';
import type { product, productToAdd } from '../types/productTypes';
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

  constructor(private route: ActivatedRoute, private _reader: DataReaderService,private router: Router) {
    this.id = this.route.snapshot.params["id"];
    _reader.GetById(this.id).then(res => {
         const prod = res as product
         this.name.setValue(prod.productName)
         this.ean.setValue(prod.ean)
         this.quantity.setValue(prod.quantity)
         this.price.setValue(prod.price)
        })
  }

  handleSubmit(){
    this._reader.Put(this.id ,{
      productName: this.name.value,
      ean: this.ean.value,
      price: this.price.value,
      quantity: this.quantity.value
    } as productToAdd)

    this.router.navigate(['/table'])
  }
}
