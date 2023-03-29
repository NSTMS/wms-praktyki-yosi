import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { user } from '@static/types/userTypes';
import { AdminPanelService } from '@services/admin-panel/admin-panel.service';
import { MatDialog } from '@angular/material/dialog';
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
    public dialog: MatDialog
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin')
      this.router.navigate(['/table']);
    this._reader.GetAll().subscribe((data) => {
      if (data != null) this.length = data.length || 0;
      else this.length = 0;

      this.dataSource = new MatTableDataSource(data);

      if (this.paginator != undefined)
        this.dataSource.paginator = this.paginator;
    });
    this.displayedColumns = [...this._reader.columns, 'edit', 'delete'];
  }

  handleDelete(id: number) {
    this._reader.Delete(id);
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/users']);
    });
  }
}
