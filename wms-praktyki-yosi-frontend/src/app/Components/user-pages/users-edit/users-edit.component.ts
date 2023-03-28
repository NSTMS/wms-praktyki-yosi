import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminPanelService } from '@services/admin-panel/admin-panel.service';
import { Form, FormControl, Validators } from '@angular/forms';
import { user } from '@static/types/userTypes';
import { catchError, map, retryWhen } from 'rxjs';
import { ErrorService } from '@app/Services/error-handling/error.service';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.scss'],
})
export class UsersEditComponent implements OnInit {
  selected: string = 'User';
  data: any;
  Id: FormControl;
  Email: FormControl;
  role: FormControl;
  constructor(
    private _reader: AdminPanelService,
    private _errorHandler: ErrorService
  ) {
    this.selected = this.data?.id;
    this.Id = new FormControl({value: '', disabled: true },
      Validators.required
    );
    this.Email = new FormControl('', Validators.required);
    this.role = new FormControl('', Validators.required);
  }
  ngOnInit() {
    this._reader.GetInfoFromToken().pipe(
      map((data) => {
        this.Id.setValue(data.id)
        this.Email.setValue(data.email)
        return;
      }),
      catchError((error) => {
        this._errorHandler.HandleBadResponse(error)
        throw error;
      })
    ).subscribe()
  }

  ngAfterContentInit(){

  }

  handleSubmit() {
    this._reader.GetById(this.Id.value).pipe(
      map((usr: user) => {
        if (this.Id.invalid || this.Email.invalid) {
          this._errorHandler.handleErrorCode(2)
          return;
        }
        const user : user = {
          Id: this.Id.value,
          Email: this.Email.value,
          PasswordHash: usr.PasswordHash,
          Role: this.selected
        }
        return this._reader.Put(this.Id.value,user)
      }),
      catchError((error) => {
        throw error;
      })
    )
  }
}
