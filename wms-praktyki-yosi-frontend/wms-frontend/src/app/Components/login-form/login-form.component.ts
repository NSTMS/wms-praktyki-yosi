import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/Services/authentication.service';
import { ErrorService } from 'src/app/Services/error.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {
  email = new FormControl("",[Validators.email,Validators.required])
  password = new FormControl("",Validators.required)

  constructor(private _errorHandler: ErrorService,private router: Router,private _authenticationService : AuthenticationService) {}
  handleSubmit()
  {  
    const token = this._authenticationService.signUp(this.email.value as string, this.password.value as string)
    token.then(res =>{
      console.log(res);
      
      if(typeof res == "number")
      {
        this._errorHandler.handleErrorCode(res);
      }
      else{
        console.log("logged");
        window.location.reload();
        this.router.navigate(["/table"])
        // this._errorHandler.handleSuccesLoginIn();
      }
    }).catch(ex=>{console.log(JSON.stringify(ex))})
    
    
    
  }
}
