import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '@services/authentication/authentication.service';
import { ErrorService } from '@services/error-handling/error.service';
import { catchError, map, tap } from 'rxjs';
type errorMessage = {
  Errors: string[];
};

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss'],
})
export class RegisterFormComponent {
  email = new FormControl('', [Validators.email, Validators.required]);
  password = new FormControl('', Validators.required);
  confirmPassword = new FormControl('', Validators.required);

  constructor(
    private _errorHandler: ErrorService,
    private router: Router,
    private _authenticationService: AuthenticationService
  ) {
    if (localStorage.getItem('token')) this.router.navigate(['/table']);
  }

  async handleButtonClick() {
    const data = await this._authenticationService.registerUser(
      this.email.value as string,
      this.password.value as string,
      this.confirmPassword.value as string
    );
    if (data) {
      this._errorHandler.handleErrorCode(8);
      this.router.navigate(['/login']);
    }
  }
}
