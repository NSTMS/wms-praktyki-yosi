import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { ErrorService } from '@services/error-handling/error.service';
import { LocationService } from '@services/fetching-services/location.service';
import {locationToEdit,productLocation,} from '@static/types/locationTypes';

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
  tag = new FormControl("");

  id: number;
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _service: LocationService,
    private _errorHandler: ErrorService
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);
    this.id = this.route.snapshot.params['id'];
    this.loadData()
  }
  async loadData(){
    const location = await this._service.GetById(this.id) as productLocation || undefined
    if (location == undefined) {
      this.router.navigate(['/table']);
      return;
    }
    this.position.setValue(location.position);
    this.quantity.setValue(location.quantity);
    this.magazineId.setValue(location.magazineId);
    this.tag.setValue(location.tag)
  }

  async handleSubmit() {
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
      tag : this.tag.value || ""
    };

    await this._service.EditLocation(this.id, newLocation)
    this.router.navigate(['/table'])
  }
}
