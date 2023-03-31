import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DataReaderService } from '@services/fetching-services/data-reader.service';
import type { product } from '@static/types/productTypes';

@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.scss'],
})
export class DataTableComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<product> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  options : string[]
  formGroup : FormGroup;
  constructor(private _reader: DataReaderService, private router: Router) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);

    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [
        ...this._reader.columns,
        'edit',
        'info',
        'delete',
      ];
    else this.displayedColumns = [...this._reader.columns];

    this.formGroup = new FormGroup({
      'search' : new FormControl(''),
      'column' : new FormControl(''),
      'descending' : new FormControl(false)
    })
    this.options = ["", ...this._reader.columns.map(o => o.trim().toLowerCase())]  
    this.options  = this.options.filter(o => o != "id")
    
  }


  async loadData() {
    const searchTerm = `${this.formGroup.value.search}&orderBy=${this.formGroup.value.column}&descending=${this.formGroup.value.descending}` 
    const data = await this._reader.GetAll(searchTerm) as product[]
      if (data != null) this.length = data.length || 0;
      else this.length = 0;

      this.dataSource = new MatTableDataSource(data);

      if (this.paginator != undefined)
        this.dataSource.paginator = this.paginator;
  }

  ngAfterContentInit() {
    this.loadData();
  }

  async handleDelete(id: number) {
    await this._reader.Delete(id)
    window.location.reload();
  }


}
