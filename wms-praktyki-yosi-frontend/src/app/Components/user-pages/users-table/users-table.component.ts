import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { user } from '@static/types/userTypes';
import { AdminPanelService } from '@services/admin-panel/admin-panel.service';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.scss'],
})
export class UsersTableComponent {
  dataSource: MatTableDataSource<user> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  constructor(
    private _reader: AdminPanelService,
    private router: Router,
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin') this.router.navigate(['/table']);
    this.displayedColumns = [...this._reader.columns, 'edit', 'delete'];
    this.loadData()
  }
  async loadData()
  {
    let data = await this._reader.GetAll() as user[]
      if (data != null) this.length = data.length || 0;
      else this.length = 0;
      data = data.filter(u => u.email !== localStorage.getItem('email'))
      this.dataSource = new MatTableDataSource(data);
      if (this.paginator != undefined)
        this.dataSource.paginator = this.paginator;
  }
  async handleDelete(id: string) {
    await this._reader.Delete(id);
    this.loadData()
    // this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    //   this.router.navigate(['/users']);
    // });
  }
}
