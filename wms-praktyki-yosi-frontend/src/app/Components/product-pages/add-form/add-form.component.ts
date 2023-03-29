import { Component } from '@angular/core';
import { DataReaderService } from '@services/fetching-services/data-reader.service';
import type { productToAdd } from '@static/types/productTypes';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorService } from '@services/error-handling/error.service';
import { catchError, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-add-form',
  templateUrl: './add-form.component.html',
  styleUrls: ['./add-form.component.scss'],
})
export class AddFormComponent {
  name = new FormControl('', Validators.required);
  ean = new FormControl('', [
    Validators.required,
    Validators.pattern('^[0-9]{13}$'),
  ]);
  price = new FormControl(0.0, [
    Validators.required,
    Validators.pattern('^[0-9]+?(.[0-9]+)?$'),
  ]);
  tag = new FormControl('');

  constructor(
    private _reader: DataReaderService,
    private router: Router,
    private _errorHandler: ErrorService
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User')
      this.router.navigate(['/table']);
  }

  handleSubmit() {
    if (this.name.invalid || this.ean.invalid || this.price.invalid) {
      this._errorHandler.handleErrorCode(2);
    } else {
      this._reader
        .Post({
          productName: this.name.value,
          ean: this.ean.value,
          price: this.price.value,
          tag: this.tag.value,
        } as productToAdd)
        .pipe(
          tap(() => {
            window.location.reload();
          }),
          catchError((error) => {
            return throwError(() => new Error(error));
          })
        )
        .subscribe();

      this.router.navigate(['/table']);
    }
  }
}
