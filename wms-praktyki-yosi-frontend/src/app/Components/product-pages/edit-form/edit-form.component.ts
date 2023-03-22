import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataReaderService } from '@services/fetching-services/data-reader.service';
import type { product, productToAdd } from '@static/types/productTypes';
import { FormControl, Validators } from '@angular/forms';
import { ErrorService } from '@services/error-handling/error.service';

@Component({
  selector: 'app-edit-form',
  templateUrl: './edit-form.component.html',
  styleUrls: ['./edit-form.component.scss'],
})
export class EditFormComponent {
  id: number;

  name = new FormControl('', Validators.required);
  ean = new FormControl('', [
    Validators.required,
    Validators.pattern('^[0-9]{13}$'),
  ]);
  price = new FormControl(0.0, [
    Validators.required,
    Validators.pattern('^[0-9]+?(.[0-9]+)?$'),
  ]);

  constructor(
    private route: ActivatedRoute,
    private _reader: DataReaderService,
    private router: Router,
    private _errorHandler: ErrorService
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User')
      this.router.navigate(['/table']);
    this.id = this.route.snapshot.params['id'];
    _reader
      .GetById(this.id)
      .then((res) => {
        const prod = res as unknown as product;
        if (typeof prod === 'number') {
          this.router.navigate(['table']);
          _errorHandler.handleErrorCode(prod);
        }
        this.name.setValue(prod.productName);
        this.ean.setValue(prod.ean);
        this.price.setValue(prod.price);
      })
      .catch((ex) => {
        console.log(ex);
        this.router.navigate(['/table']);
      });
  }

  handleSubmit() {
    if (this.name.invalid || this.ean.invalid || this.price.invalid) {
      this._errorHandler.handleErrorCode(2);
    } else {
      this._reader.Put(this.id, {
        productName: this.name.value,
        ean: this.ean.value,
        price: this.price.value,
      } as productToAdd);
      this.router.navigate(['/table']);
    }
  }
}
