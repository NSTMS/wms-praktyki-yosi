import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ErrorService } from '@services/error.service';
import { LocationService } from '@services/location.service';
import { productLocation } from '@static/types/locationTypes';

@Component({
  selector: 'app-edit-location',
  templateUrl: './edit-location.component.html',
  styleUrls: ['./edit-location.component.scss']
})
export class EditLocationComponent {
  position = new FormControl('',[ Validators.required ,Validators.pattern('^[A-Z]+\d+\/\d+$')]);

  quantity = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);

  id: number
  constructor(private route: ActivatedRoute, private router: Router, private _service: LocationService, private _errorHandler: ErrorService) {
    this.id = this.route.snapshot.params['id'];
    _service
    .GetById(this.id)
    .then((location : productLocation) => {
      if (typeof location === 'number') {
        this.router.navigate(['/table']);
        this._errorHandler.handleErrorCode(location);
      }
      this.position.setValue(location.position);
      this.quantity.setValue(location.quantity);

    })
    .catch((ex) => {
      console.log(ex);
      this.router.navigate(['/table']);
    });

  }

  handleSubmit() {

  }
}
