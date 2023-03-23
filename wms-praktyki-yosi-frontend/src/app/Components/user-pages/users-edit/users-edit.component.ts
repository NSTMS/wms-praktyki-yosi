import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AdminPanelService } from '@services/admin-panel/admin-panel.service';
import { Form, FormControl, Validators } from '@angular/forms';
import { user } from '@static/types/userTypes';

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
    private route: ActivatedRoute
  ) {
    const temp = this.data;

    this.selected = this.data?.id;
    this.Id = new FormControl(
      { value: this.route.snapshot.paramMap.get('id'), disabled: true },
      Validators.required
    );
    this.Email = new FormControl(this.data?.email, Validators.required);
    this.role = new FormControl('', Validators.required);
  }
  async ngOnInit() {
    this.data = await this._reader.GetInfoFromToken();
    // console.log(this.data);
  }

  handleSubmit() {
    // let tempUser = this._reader.GetById()
    // this._reader.Put()
  }
  handleSelectChange() {
    console.log(this.selected);
  }
}
