import { Component } from '@angular/core';
import { MatTableDataSource, MatTable, MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { DataReaderService } from 'src/app/Services/data-reader.service';
import type { product } from 'src/app/types/productTypes';

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
    if(localStorage.getItem("token") == null) this.router.navigate(["/login"])
    this.canAddAndDel = localStorage.getItem("role") != "User"
    this._reader.GetAll().then((res)=>{
      try{
        this.length = res.length
      }
      catch{
        this.length = 0
      }
      this.dataSource = new MatTableDataSource(res)
    } )
    if(this.canAddAndDel)
    {
      this.displayedColumns = [...this._reader.columns, "edit", "delete"]
    }
    else this.displayedColumns = [...this._reader.columns]
  } 

  handleDelete(id: number){
   this._reader.Delete(id)
   this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
    this.router.navigate(["/table"]);
});
  }
}
