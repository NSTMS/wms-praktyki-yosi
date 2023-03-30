import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from '@services/fetching-services/location.service';
import { DataReaderService } from '@services/fetching-services/data-reader.service';
import { ErrorService } from '@services/error-handling/error.service';
import { product } from '@static/types/productTypes';
import { productLocation } from '@static/types/locationTypes';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { catchError, from, map, Observable, tap, throwError } from 'rxjs';
@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss'],
})
export class ProductInfoComponent {
  id: number;
  product: product = {} as product;
  dataSource = new MatTableDataSource<productLocation>();
  displayedColumns: string[];
  canAddAndDel: boolean;
  magazineId: number;

  constructor(
    private route: ActivatedRoute,
    private _reader: DataReaderService,
    private _locationService: LocationService,
    private _magazineService: MagazineService,
    private router: Router,
    private _errorHandler: ErrorService
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);

    this.magazineId = this.route.snapshot.params['magazineId'];
    this.id = this.route.snapshot.params['id'];

    this.canAddAndDel = localStorage.getItem('role') != 'User';
    if (this.canAddAndDel)
      this.displayedColumns = [..._locationService.columns, 'edit', 'delete'];
    else this.displayedColumns = _locationService.columns;

    this.loadData();
  }

  async loadData() {
    let productPromise;
    if (!this.magazineId) {
      productPromise = this._reader.GetById(this.id);
    } else {
      productPromise = this._magazineService.GetLocations(
        this.id,
        this.magazineId
      );
    }

    const data = await productPromise as product
    this.product = data;
    this.dataSource = new MatTableDataSource<productLocation>(
      data.locations
    );
  }

  async handleDelete(id: number) {
    await this._locationService.Delete(id)
  }
}