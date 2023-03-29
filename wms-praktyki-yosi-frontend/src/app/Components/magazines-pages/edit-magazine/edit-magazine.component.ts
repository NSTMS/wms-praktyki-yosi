import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from '@app/Services/error-handling/error.service';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import {
  magazine,
  magazineToAdd,
  magazineToEdit,
} from '@static/types/magazineTypes';
import { catchError, map, tap, throwError } from 'rxjs';

@Component({
  selector: 'app-edit-magazine',
  templateUrl: './edit-magazine.component.html',
  styleUrls: ['./edit-magazine.component.scss'],
})
export class EditMagazineComponent {
  name = new FormControl('', [Validators.required]);

  address = new FormControl('', [Validators.required]);

  id: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _magazineService: MagazineService,
    private _errorHandler: ErrorService
  ) {
    this.id = this.route.snapshot.params['id'];
    _magazineService
      .GetById(this.id)
      .pipe(
        map((location: magazineToEdit | undefined) => {
          if (location == undefined) {
            this.router.navigate(['/magazines']);
            return;
          }
          this.name.setValue(location.name);
          this.address.setValue(location.address);
        }),
        catchError((error) => {
          this.router.navigate(['/table']);
          _errorHandler.handleErrorCode(error);
          return error;
        })
      )
      .subscribe();
  }

  handleSubmit() {
    if (this.name.invalid || this.address.invalid) {
      this._errorHandler.handleErrorCode(2);
      return;
    }
    const newMagazine: magazineToEdit = {
      name: this.name.value || '',
      address: this.address.value || '',
    };
    this._magazineService
      .Edit(this.id, newMagazine)
      .pipe(
        tap(() => {
          this.router.navigate(['/magazines']);
        })
      )
      .subscribe();
  }
}
