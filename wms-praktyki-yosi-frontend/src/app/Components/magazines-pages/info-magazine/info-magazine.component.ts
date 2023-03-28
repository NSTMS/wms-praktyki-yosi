import { HttpErrorResponse } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DataReaderService } from '@app/Services/fetching-services/data-reader.service';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { magazine, magazineToAdd, magazineToEdit } from '@static/types/magazineTypes';
import { product } from '@static/types/productTypes';
import { catchError, tap,map, throwError } from 'rxjs';

@Component({
  selector: 'app-info-magazine',
  templateUrl: './info-magazine.component.html',
  styleUrls: ['./info-magazine.component.scss'],
})
export class InfoMagazineComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<product> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  id: number;
  magazine: magazineToAdd = {
    name: '',
    address: '',
    dimentions: '',
    shelvesPerRow: 0,
    maxShelfQuantity:0
  };

  constructor(
    private _magazineService: MagazineService,
    private router: Router,
    private route: ActivatedRoute,
    private _reader: DataReaderService
  ) {
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

    this.id = this.route.snapshot.params['id'];


    this._magazineService.GetById(this.id).pipe(
      map((magazine : magazineToAdd)=>{      
        this.magazine = magazine
        if (!magazine) {
          this.router.navigate(['/magazines']);
        }
      }),
      catchError(()=>this.router.navigate(['/magazines']))
    ).subscribe()
  }

  loadData() {
    return this._magazineService.GetAllProducts(this.id).subscribe((data) => {
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

  handleDelete(productId: number) {
    return this._reader.Delete(productId).subscribe(
      tap(() =>{
        this.loadData();
      }),
      (error)=>{
        console.log(error); 
      }
    )
  }
}

