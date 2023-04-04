import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { locationToAdd } from '@static/types/locationTypes';
import { detailedShelf } from '@static/types/shelfTypes';
@Component({
  selector: 'app-shelf-detail',
  templateUrl: './shelf-detail.component.html',
  styleUrls: ['./shelf-detail.component.scss']
})
export class ShelfDetailComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<locationToAdd> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  id: number;
  position : string;
  shelf: detailedShelf = {
    id : 0,
    position: "",
    maxQuantity: 0,
    totalQuantity: 0,
    freeSpace:0,
    locations: [{
      position:"",
      magazineId: 0,
      quantity: 0,
      productId: 0,
      tag: ""
    }]
  }
  constructor(
    private _magazineService: MagazineService,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);
    
    this.displayedColumns = ['productId','magazineId','position','quantity','tag'];

    this.id = this.route.snapshot.params['id'];
    this.position = this.route.snapshot.params['position'];
  }

  async loadData() {
    const data = await this._magazineService.GetShelfDetails(this.id, this.position) as detailedShelf
    if (data != null) this.length = data.locations.length || 0;
    else this.length = 0;

    this.shelf = data
    this.dataSource = new MatTableDataSource(data.locations);
    if (this.paginator != undefined)
      this.dataSource.paginator = this.paginator;
  }

  ngAfterContentInit() {
    this.loadData();
  }
}
