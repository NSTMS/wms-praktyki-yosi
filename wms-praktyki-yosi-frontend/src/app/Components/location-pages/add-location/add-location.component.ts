import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from '@services/error-handling/error.service';
import { LocationService } from '@services/fetching-services/location.service';
import {
  productLocation,
  locationToEdit,
  locationToAdd,
} from '@static/types/locationTypes';
import { catchError, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-add-location',
  templateUrl: './add-location.component.html',
  styleUrls: ['./add-location.component.scss'],
})
export class AddLocationComponent {
  productId = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);

  magazineId = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);

  position = new FormControl('', [
    Validators.required,
    Validators.pattern('^[A-Z]+[0-9]+/[0-9]+$'),
  ]);

  quantity = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);

  tag = new FormControl('');

  constructor(
    private router: Router,
    private _service: LocationService,
    private _errorHandler: ErrorService
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);
  }

  async handleSubmit() {
    if (
      this.position.invalid ||
      this.quantity.invalid ||
      this.productId.invalid ||
      this.magazineId.invalid
    ) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newLocation: locationToAdd = {
      position: this.position.value || '',
      magazineId: this.magazineId.value || -1,
      quantity: this.quantity.value || 0,
      productId: this.productId.value || -1,
      tag: this.tag.value || '',
    };

    await this._service.AddLocation(newLocation)
    this.router.navigate([`/info/${this.productId.value}`]);
  }
}
