import { Component } from '@angular/core';
import { MatTableDataSource, MatTable, MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';

import { DataReaderService } from '@services/data-reader.service';
import type { product } from '@static/types/productTypes';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent{
  dataSource: MatTableDataSource<product> = new MatTableDataSource();
  displayedColumns: string[];
  length : number = 0
  canAddAndDel : boolean;

  constructor ( private _reader: DataReaderService,private router: Router) {

    if(localStorage.getItem("token") == null)
      this.router.navigate(["/login"])

    this.canAddAndDel = localStorage.getItem("role") != "User"

    this._reader.GetAll().then((res)=>{
      if (res)
        this.length = res.length || 0
      else
        this.length = 0
      this.dataSource = new MatTableDataSource(res)
    })
    if(this.canAddAndDel)
      this.displayedColumns = [...this._reader.columns, "edit", "info", "delete"]
    else
      this.displayedColumns = [...this._reader.columns]
  }
  ngAfterContentInit()  {
    this._reader.GetAll().then((res)=>{
      if (res)
        this.length = res.length || 0
      else
        this.length = 0
      this.dataSource = new MatTableDataSource(res)
    })
  }

  handleDelete(id: number){
    this._reader.Delete(id)
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
      this.router.navigate(["/table"]);
    });
  }
}
