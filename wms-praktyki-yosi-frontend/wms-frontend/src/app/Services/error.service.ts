import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material/snack-bar';
declare var require: any;
const errorCodes = require("src/static/error-codes.json")

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor(private _snackBar: MatSnackBar){}


  handleErrorCode(code : number){
    this._snackBar.open(errorCodes[(localStorage.getItem('lang') as string).toLowerCase()][code.toString()],"ok")
    // if(localStorage.getItem('token')) localStorage.removeItem('token')

  }
  handleSuccesLoginIn(){
    this._snackBar.open("zalogowano pomyślnie!","ok")
    localStorage.setItem('token',"token")

  }
}
