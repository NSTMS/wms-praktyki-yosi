import { Component } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { LocationService } from '@app/Services/location.service';
import { DataReaderService } from '@services/data-reader.service';
import { ErrorService } from '@services/error.service';
import { product } from '@static/types/productTypes';
import { productLocation } from '@static/types/locationTypes';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss']
})
export class ProductInfoComponent {
  id: number
  product: product = {} as product
  dataSource = new MatTableDataSource<productLocation>()
  displayedColumns: string[]
  canAddAndDel: boolean

  constructor(
    private route: ActivatedRoute,
    private _reader: DataReaderService,
    private _locationService: LocationService,
    private router: Router,
    private _errorHandler: ErrorService
    ) {
    if(localStorage.getItem("token") == null) this.router.navigate(["/login"])

    this.id = this.route.snapshot.params['id'];

    this.canAddAndDel = localStorage.getItem("role") != "User"
    if (this.canAddAndDel)
      this.displayedColumns = [..._locationService.columns, "edit", "delete"]
    else
      this.displayedColumns = _locationService.columns

    this.loadData()

  }

  loadData(){
    this._reader
      .GetById(this.id)
      .then((prod: product) => {
        if (typeof prod === 'number') {
          this.router.navigate(['/table']);
          this._errorHandler.handleErrorCode(prod);
        }
        this.product = prod
        this.dataSource = new MatTableDataSource<productLocation>(prod.locations)

      })
      .catch((ex) => {
        console.log(ex);
        this.router.navigate(['/table']);
      });
  }


  async handleDelete(id : number) {
    const deleted = await this._locationService.Delete(id);
    if (!deleted)
      return;

    this.loadData()

    this._errorHandler.errorMessageShow(["Pomyślnie usunieto", "ok"])
  }
}
