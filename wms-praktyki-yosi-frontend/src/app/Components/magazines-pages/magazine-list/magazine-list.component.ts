import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MagazineService } from '@services/fetching-services/magazine.service';
import { magazine } from '@static/types/magazineTypes';

@Component({
  selector: 'app-magazine-list',
  templateUrl: './magazine-list.component.html',
  styleUrls: ['./magazine-list.component.scss'],
})
export class MagazineListComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<magazine> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  options : string[]
  formGroup : FormGroup;

  constructor(
    private _magazineService: MagazineService,
    private router: Router
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);

    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [
        ...this._magazineService.columns,
        'edit',
        'info',
        'delete',
      ];
    else this.displayedColumns = [...this._magazineService.columns];
    this.formGroup = new FormGroup({
      'search' : new FormControl(''),
      'column' : new FormControl(''),
      'descending' : new FormControl(false)
    })
    
    this.options = ["","name"]  
    this.options  = this.options.filter(o => o != "guid")
  }

  async loadData() {
    const searchTerm = `${this.formGroup.value.search}&orderBy=${this.formGroup.value.column}&descending=${this.formGroup.value.descending}` 
    const data = await this._magazineService.GetAll(searchTerm) as magazine[]
     
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
    await this._magazineService.Delete(id)
    this.loadData();
  }
}
