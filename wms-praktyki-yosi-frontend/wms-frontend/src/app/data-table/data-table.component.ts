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
  _reader: DataReaderService;
  displayedColumns: string[];

  constructor () {
    this._reader = new DataReaderService();
    this.dataSource = new MatTableDataSource(this._reader.GetAll())
    this.displayedColumns = ["id", "productName", "EAN", "price", "quantity"]
  }

}
