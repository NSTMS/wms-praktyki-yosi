import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { ErrorService } from 'src/app/Services/error.service';
import { Router } from '@angular/router';
import type { Token } from '@static/types/tokenTypes';
import { GlobalService } from '@app/Services/global.service';
@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent {
  email = new FormControl('', [Validators.email, Validators.required]);
  password = new FormControl('', Validators.required);

  constructor(
    private _errorHandler: ErrorService,
    private router: Router,
    private _authenticationService: AuthenticationService,
    private globalSrv: GlobalService
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('token')) this.router.navigate(['/table']);
  }
  async handleSubmit() {
    const token = await this._authenticationService.logIn(
      this.email.value as string,
      this.password.value as string
    );

    if (token == null) {
      this._errorHandler.handleErrorCode(3);
    } else {
      localStorage.setItem('token', (token as Token).token);
      this.router.navigate(['/table']);
      // window.location.reload()
      console.log('logged');
      this.globalSrv.theRole = (token as Token).role;

      this._errorHandler.handleSuccesLoginIn();
    }
  }
}
