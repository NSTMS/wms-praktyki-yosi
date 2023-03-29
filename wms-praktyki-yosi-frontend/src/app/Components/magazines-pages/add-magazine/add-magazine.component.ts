import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ErrorService } from '@app/Services/error-handling/error.service';
import { LocationService } from '@app/Services/fetching-services/location.service';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { locationToAdd } from '@static/types/locationTypes';
import { magazineToAdd } from '@static/types/magazineTypes';
import { tap, catchError, throwError, map } from 'rxjs';

@Component({
  selector: 'app-add-magazine',
  templateUrl: './add-magazine.component.html',
  styleUrls: ['./add-magazine.component.scss'],
})
export class AddMagazineComponent {
  name = new FormControl('', [Validators.required]);

  address = new FormControl('', [Validators.required]);

  dimentions = new FormControl('', [
    Validators.required,
    Validators.pattern('^[0-9]{1,2}x[0-9]+$'),
  ]);

  shelvesPerRow = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);
  maxShelfLoad = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);
  constructor(
    private router: Router,
    private _magazineService: MagazineService,
    private _errorHandler: ErrorService
  ) {}

  handleSubmit() {
    if (this.name.invalid || this.address.invalid) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newMagazine: magazineToAdd = {
      name: this.name.value || '',
      address: this.address.value || '',
      shelvesPerRow: this.shelvesPerRow.value || -1,
      dimentions: this.dimentions.value || '',
      maxShelfLoad: this.maxShelfLoad.value || 0,
    };

    this._magazineService
      .Add(newMagazine)
      .pipe(
        map((res) => {
          if (!res) return;
          this.router.navigate([`/magazines`]);
        }),
        catchError((error) => {
          return throwError(() => new Error(error));
        })
      )
      .subscribe();
  }
}
