import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DataReaderService } from '@services/fetching-services/data-reader.service';
import type { product, productToAdd } from '@static/types/productTypes';
import { FormControl, Validators } from '@angular/forms';
import { ErrorService } from '@services/error-handling/error.service';
import { catchError, map, tap, throwError } from 'rxjs';

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
    this.loadData()
  }
  async loadData()
  {
    const data = await this._reader.GetById(this.id) as product
      this.name.setValue(data.productName);
      this.ean.setValue(data.ean);
      this.price.setValue(data.price);
  }

 async handleSubmit() {
    if (this.name.invalid || this.ean.invalid || this.price.invalid) {
      this._errorHandler.handleErrorCode(2);
    } else {
      const updatedProduct: productToAdd = {
        productName: this.name.value || '',
        ean: this.ean.value || '',
        price: this.price.value || 0,
      };

      await this._reader.Put(this.id, { ...updatedProduct })
      this.router.navigate(['/table'])
    }
  }
}
