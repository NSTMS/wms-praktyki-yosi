import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { ErrorService } from 'src/app/Services/error.service';

type errorMessage = {
  Errors: string[]
}

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['./register-form.component.scss']
})
export class RegisterFormComponent {
  email = new FormControl("", [Validators.email, Validators.required])
  password = new FormControl("", Validators.required)
  confirmPassword = new FormControl("", Validators.required)

  constructor(private _errorHandler: ErrorService, private router: Router, private _authenticationService: AuthenticationService) { }
  async handleButtonClick() {
    const token = this._authenticationService.registerUser(this.email.value as string, this.password.value as string, this.confirmPassword.value as string)
    token.then(response => response?.json()).then(res => {
      try {
        this._errorHandler.errorMessageShow((res as unknown as errorMessage).Errors);
      }
      catch {
        if (res.ok) {
          this._errorHandler.handleErrorCode(8)

        this.router.navigate(["/login"])
        }
      }
    }).catch(ex=> { console.log(ex)})

  }
}
