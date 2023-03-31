import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErrorService } from '@app/Services/error-handling/error.service';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import {magazineToEdit} from '@static/types/magazineTypes';

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
    this.loadData()
  }

  async loadData(){
   const location = await this._magazineService.GetById(this.id) as  magazineToEdit
   if (location == undefined) {
    this.router.navigate(['/magazines']);
    return;
    }
    this.name.setValue(location.name);
    this.address.setValue(location.address);

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
    this._magazineService.Edit(this.id, newMagazine)
    this.router.navigate(['/magazines']);
  }
}
