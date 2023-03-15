import { Component } from '@angular/core';
import { MatTableDataSource, MatTable, MatTableModule } from '@angular/material/table';
import { DataReaderService } from '../data-reader.service';
import type { product, productToAdd } from '../types/productTypes';


@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss']
})
export class DataTableComponent {
  dataSource: MatTableDataSource<product>;
  displayedColumns: string[];

  constructor ( private _reader: DataReaderService) {
    this.dataSource = new MatTableDataSource(this._reader.GetAll())
    this.displayedColumns = [...this._reader.columns, "edit", "delete"]
  }
  handleDelete(id: number){
   this._reader.Delete(id)
   console.log(this._reader.GetAll())
  }
}
