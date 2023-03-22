import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
declare var require: any;
const errorCodes = require('src/static/error-codes.json');

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

  async HandleBadResponse(respone: Response) {
    try {
      const responseData = await respone.json();

      const errors = responseData.errors as string[];
      this.errorMessageShow(errors);
    } catch {
      console.error('Status: ', respone.status, respone.statusText);

      if (respone.status == 404 || respone.status >= 500)
        this.handleErrorCode(5);
    }
  }
}
