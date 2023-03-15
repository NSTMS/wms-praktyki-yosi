import { DataSource } from '@angular/cdk/collections';
import { Component } from '@angular/core';
import { MatTableDataSource, MatTable, MatTableModule } from '@angular/material/table';
import { Router } from '@angular/router';
import { DataReaderService } from '../data-reader.service';
import type { product, productToAdd } from '../types/productTypes';


@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss'] 
})
export class DataTableComponent {
  dataSource: MatTableDataSource<product> = new MatTableDataSource();
  displayedColumns: string[];

  constructor ( private _reader: DataReaderService,private router: Router) {
    this._reader.GetAll().then((res)=>{
      console.log(res);
      
      this.dataSource = new MatTableDataSource(res)
    } )
    this.displayedColumns = [...this._reader.columns, "edit", "delete"]
  }
  handleDelete(id: number){
   this._reader.Delete(id)
   this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
    this.router.navigate(["/table"]);
});
   console.log(this._reader.GetAll())
  }
}
