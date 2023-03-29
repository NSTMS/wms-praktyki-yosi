import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
declare var require: any;
const errorCodes = require('static/error-codes.json');

@Injectable({
  providedIn: 'root',
})
export class ErrorService {
  constructor(private _snackBar: MatSnackBar) {}

  handleErrorCode(code: number) {
    this._snackBar.open(
      errorCodes[(localStorage.getItem('lang') as string).toLowerCase()][
        code.toString()
      ],
      'ok'
    );
  }

  handleSuccesLoginIn() {
    this._snackBar.open('zalogowano pomyślnie!', 'ok');
  }

  errorMessageShow(arr: string[]) {
    if (!arr) return;
    console.log(arr);

    let text = '';
    var set = new Set(arr);

    set.forEach((val) => {
      text +=
        errorCodes[(localStorage.getItem('lang') as string).toLowerCase()][
          val
        ] + '\n';
    });
    this._snackBar.open(text, 'ok');
  }

  HandleBadResponse(response: HttpErrorResponse) {
    if (response.status == 404 || response.status >= 500)
      this.handleErrorCode(5);
    const errors = response.error as string[];
    this.errorMessageShow(errors);
  }
}
