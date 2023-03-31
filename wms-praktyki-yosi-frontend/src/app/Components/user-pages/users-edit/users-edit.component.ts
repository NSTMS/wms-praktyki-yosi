import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminPanelService } from '@services/admin-panel/admin-panel.service';
import { FormControl, Validators } from '@angular/forms';
import { user } from '@static/types/userTypes';
import { ErrorService } from '@app/Services/error-handling/error.service';

@Component({
  selector: 'app-users-edit',
  templateUrl: './users-edit.component.html',
  styleUrls: ['./users-edit.component.scss'],
})
export class UsersEditComponent implements OnInit {
  selected: string = 'User';
  id: string;
  Id: FormControl;
  Email: FormControl;
  role: FormControl;
  constructor(
    private _reader: AdminPanelService,
    private route: ActivatedRoute,
    private _errorHandler: ErrorService,
    private router : Router
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin') this.router.navigate(['/table']);
    
    this.id = this.route.snapshot.params['id'];
    this.Id = new FormControl({ value: '', disabled: true }, Validators.required);
    this.Email = new FormControl({ value: '', disabled: true }, Validators.required);
    this.role = new FormControl('', Validators.required);
  }
  async ngOnInit() {

    const data = await this._reader.GetById(this.id) as user
    this.Id.setValue(data.id);
    this.Email.setValue(data.email);    
  }

  ngAfterContentInit() {}

  async handleSubmit() {    
    if (this.Id.invalid || this.Email.invalid) {
      this._errorHandler.handleErrorCode(2);
      return;
    }
    await this._reader.Put(this.Id.value, JSON.stringify(this.selected));
    this.router.navigate(['/users'])
  }
}
