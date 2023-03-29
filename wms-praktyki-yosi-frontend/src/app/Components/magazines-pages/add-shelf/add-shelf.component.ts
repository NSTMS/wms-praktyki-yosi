import { Component } from '@angular/core';
import { ErrorService } from '@services/error-handling/error.service';
import { catchError, map, tap, throwError } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, Validators } from '@angular/forms';
import { newShelf } from '@static/types/shelfTypes';

@Component({
  selector: 'app-add-shelf',
  templateUrl: './add-shelf.component.html',
  styleUrls: ['./add-shelf.component.scss'],
})
export class AddShelfComponent {
  magazineId = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);
  position = new FormControl('', [
    Validators.required,
    Validators.pattern('^[A-Z]+[0-9]+/[0-9]+$'),
  ]);
  maxload = new FormControl(0, [
    Validators.required,
    Validators.pattern('^[0-9]+$'),
  ]);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private _errorHandler: ErrorService
  ) {}

  handleSubmit() {
    if (
      this.position.invalid ||
      this.maxload.invalid ||
      this.magazineId.invalid
    ) {
      this._errorHandler.handleErrorCode(2);
      return;
    }

    const shelf: newShelf = {
      magazineId: this.magazineId.value || 0,
      maxload: this.maxload.value || 0,
      position: this.position.value || '',
    };

    // jak coś to dodamy a jak nie to nie
  }
}
