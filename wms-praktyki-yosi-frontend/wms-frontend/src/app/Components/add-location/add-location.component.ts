import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from '@app/Services/error.service';
import { LocationService } from '@app/Services/location.service';
import { productLocation, locationToEdit, locationToAdd } from '@static/types/locationTypes';

@Component({
  selector: 'app-add-location',
  templateUrl: './add-location.component.html',
  styleUrls: ['./add-location.component.scss']
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
    Validators.pattern('^[A-Z]+[0-9]+\/[0-9]+$'),
  ]);

  quantity = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);

  constructor(
    private router: Router,
    private _service: LocationService,
    private _errorHandler: ErrorService
  ) {

  }

  async handleSubmit() {
    if (
      this.position.invalid ||
      this.quantity.invalid ||
      this.productId.invalid)
    {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newLocation: locationToAdd = {
      position: this.position.value || '',
      quantity: this.quantity.value || 0,
      productId: this.productId.value || -1
    };

    const added = await this._service.AddLocation(newLocation);

    if (!added)
      return;

    const productId = this.productId.value
    this.router.navigate([`/info/${productId}`]);
  }
}
