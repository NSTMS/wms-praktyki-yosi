import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ErrorService } from '@services/error.service';
import { LocationService } from '@services/location.service';
import {
  locationToAdd,
  locationToEdit,
  productLocation,
} from '@static/types/locationTypes';

@Component({
  selector: 'app-edit-location',
  templateUrl: './edit-location.component.html',
  styleUrls: ['./edit-location.component.scss'],
})
export class EditLocationComponent {
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

  id: number;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _service: LocationService,
    private _errorHandler: ErrorService
  ) {
    this.id = this.route.snapshot.params['id'];
    _service
      .GetById(this.id)
      .then((location: productLocation | undefined) => {
        if (location == undefined) {
          this.router.navigate(['/table']);
          return;
        }
        this.position.setValue(location.position);
        this.quantity.setValue(location.quantity);
      })
      .catch((ex) => {
        console.error(`edit location ex`, ex);
        this.router.navigate(['/table']);
      });
  }

  async handleSubmit() {
    if (this.position.invalid || this.quantity.invalid) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newLocation: locationToEdit = {
      position: this.position.value || '',
      quantity: this.quantity.value || 0,
    };

    const edited = await this._service.EditLocation(this.id, newLocation);

    if (edited) this.router.navigate(['/table']);
  }
}
