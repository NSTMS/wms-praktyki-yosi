import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from '@services/authentication/authentication.service';
import { ErrorService } from '@services/error-handling/error.service';
import { Router } from '@angular/router';
import type { Token } from '@static/types/tokenTypes';
import { GlobalService } from '@services/global/global.service';

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
    if (localStorage.getItem('token')) this.router.navigate(['/table']);
  }
  async handleSubmit() {
    const data: Token = (await this._authenticationService.logIn(
      this.email.value as string,
      this.password.value as string
    )) as Token;

    if (data) {
      this.globalSrv.theRole = data.role;
      this._errorHandler.handleSuccesLoginIn();
      localStorage.setItem('token', data.token);
      localStorage.setItem('email',this.email.value as string);
      this.router.navigate(['/table']);
    }
  }
}
