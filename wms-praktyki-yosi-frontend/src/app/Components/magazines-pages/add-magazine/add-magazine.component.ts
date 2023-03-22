import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorService } from '@app/Services/error-handling/error.service';
import { LocationService } from '@app/Services/fetching-services/location.service';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { locationToAdd } from '@static/types/locationTypes';
import { magazineToAdd } from '@static/types/magazineTypes';

@Component({
  selector: 'app-add-magazine',
  templateUrl: './add-magazine.component.html',
  styleUrls: ['./add-magazine.component.scss'],
})
export class AddMagazineComponent {
  name = new FormControl('', [Validators.required]);

  address = new FormControl('', [Validators.required]);

  constructor(
    private router: Router,
    private _magazineService: MagazineService,
    private _errorHandler: ErrorService
  ) {}

  async handleSubmit() {
    if (this.name.invalid || this.address.invalid) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newMagazine: magazineToAdd = {
      name: this.name.value || '',
      address: this.address.value || '',
    };

    const added = await this._magazineService.Add(newMagazine);

    if (!added) return;

    this.router.navigate([`/magazines`]);
  }
}
