import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { user } from '@static/types/userTypes';
import { AdminPanelService } from '@services/admin-panel/admin-panel.service';
import {MatDialog} from '@angular/material/dialog';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss']
})
export class UsersTableComponent {
  dataSource: MatTableDataSource<user> = new MatTableDataSource();
  displayedColumns: string[];
  length : number = 0

  constructor ( private _reader: AdminPanelService,private router: Router,public dialog: MatDialog) {
    if(localStorage.getItem("token") == null) this.router.navigate(["/login"])
    if(localStorage.getItem("role") != "Admin") this.router.navigate(["/table"])

    this._reader.GetAll().then((res)=>{
      try{
        this.length = res.length
      }
      catch{
        this.length = 0
      }
      this.dataSource = new MatTableDataSource(res)
    } )

      this.displayedColumns = [...this._reader.columns, "edit", "delete"]
  }

  handleDelete(id: number){
   this._reader.Delete(id)
   this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
    this.router.navigate(["/users"]);
});
  }
}
