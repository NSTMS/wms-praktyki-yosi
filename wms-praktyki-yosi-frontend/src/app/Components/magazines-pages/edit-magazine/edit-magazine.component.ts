import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from '@app/Services/error-handling/error.service';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { magazine, magazineToAdd } from '@static/types/magazineTypes';

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
      .then((location: magazine | undefined) => {
        if (location == undefined) {
          this.router.navigate(['/magazines']);
          return;
        }
        this.name.setValue(location.name);
        this.address.setValue(location.address);
      })
      .catch((ex) => {
        console.error(`edit magazine ex`, ex);
        this.router.navigate(['/table']);
      });
  }

  async handleSubmit() {
    if (this.name.invalid || this.address.invalid) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const newMagazine: magazineToAdd = {
      name: this.name.value || '',
      address: this.address.value || '',
    };

    const added = await this._magazineService.Edit(this.id, newMagazine);

    if (!added) return;

    this.router.navigate([`/magazines`]);
  }
}
