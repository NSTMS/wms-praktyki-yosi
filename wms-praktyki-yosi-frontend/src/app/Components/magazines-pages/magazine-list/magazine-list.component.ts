import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DataReaderService } from '@app/Services/fetching-services/data-reader.service';
import { MagazineService } from '@services/fetching-services/magazine.service';
import { magazine } from '@static/types/magazineTypes';
import { product } from '@static/types/productTypes';
import { catchError, tap, throwError } from 'rxjs';

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

  constructor(
    private _magazineService: MagazineService,
    private router: Router
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);

    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [
        ...this._magazineService.columns,
        'edit',
        'info',
        'delete',
      ];
    else this.displayedColumns = [...this._magazineService.columns];
  }

  private loadData() {

    return this._magazineService.GetAll().subscribe((data) => {
      if (data != null) 
        this.length = data.length || 0; 
      else 
        this.length = 0;

      this.dataSource = new MatTableDataSource(data);

      if (this.paginator != undefined)
        this.dataSource.paginator = this.paginator;
      });
  }

  ngAfterContentInit() {
    this.loadData();
  }

  handleDelete(id: number) {
    return this._magazineService.Delete(id).pipe(
      tap(() => {
        setTimeout(this.loadData, 1000);
      }),
      catchError((error) => {
        return throwError(() => new Error(error));
      })
    ).subscribe();
  }
}
