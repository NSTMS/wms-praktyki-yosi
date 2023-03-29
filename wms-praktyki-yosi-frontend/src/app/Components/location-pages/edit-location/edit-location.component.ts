import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ErrorService } from '@services/error-handling/error.service';
import { LocationService } from '@services/fetching-services/location.service';
import {
  locationToAdd,
  locationToEdit,
  productLocation,
} from '@static/types/locationTypes';
import { catchError, map, tap, throwError } from 'rxjs';

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
    Validators.pattern('^[A-Z]+[0-9]+/[0-9]+$'),
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
      .pipe(
        catchError((error) => {
          this.router.navigate(['/table']);
          throw error;
        })
      )
      .subscribe((location: productLocation | undefined) => {
        if (location == undefined) {
          this.router.navigate(['/table']);
          return;
        }
        this.position.setValue(location.position);
        this.quantity.setValue(location.quantity);
        this.magazineId.setValue(location.magazineId);
      });
  }

  handleSubmit() {
    if (
      this.position.invalid ||
      this.quantity.invalid ||
      this.magazineId.invalid
    ) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newLocation: locationToEdit = {
      position: this.position.value || '',
      quantity: this.quantity.value || 0,
      magazineId: this.magazineId.value || -1,
    };

    this._service
      .EditLocation(this.id, newLocation)
      .pipe(
        tap(() => this.router.navigate(['/table'])),
        catchError((error) => {
          return throwError(() => new Error(error));
        })
      )
      .subscribe();
  }
}
