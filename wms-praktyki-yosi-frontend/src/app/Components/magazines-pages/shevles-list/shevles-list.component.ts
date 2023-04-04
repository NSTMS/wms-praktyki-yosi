import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { MagazineService } from '@app/Services/fetching-services/magazine.service';
import { shelfDto } from '@static/types/shelfTypes';
import { MoveDialogComponent } from './move-dialog/move-dialog.component';
import { getLocaleMonthNames } from '@angular/common';

@Component({
  selector: 'app-shevles-list',
  templateUrl: './shevles-list.component.html',
  styleUrls: ['./shevles-list.component.scss']
})
export class ShevlesListComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<shelfDto> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  id: number;

  
  constructor(
    private _magazineService: MagazineService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);
    
    this.displayedColumns = ['id','position','maxQuantity','totalQuantity','freeSpace','info','move'];

    this.id = this.route.snapshot.params['id'];
  
  }

  async loadData() {
    const shelves = await this._magazineService.showShelves(this.id) as shelfDto[]
    if (shelves != null) this.length = shelves.length || 0;
    else this.length = 0;
    this.dataSource = new MatTableDataSource(shelves);
    if (this.paginator != undefined)
      this.dataSource.paginator = this.paginator;
      
  }

  ngAfterContentInit() {
    this.loadData();
  }

  handleDetailsBtnClick(position : string){
    position = encodeURIComponent(position)
    this.router.navigate([`/magazines/info/${this.id}/shelves/${position}`]);
  }
  handleMoveBtnClick(position: string){
    const dialogRef = this.dialog.open(MoveDialogComponent, {
      data: ""
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })
    dialogRef.afterClosed().subscribe(async result=> {     
      console.log(result);
      
      position = encodeURIComponent(position)
      await this._magazineService.ChnageShelfProductLocation(this.id, position, result as string)
          
      window.location.reload()
    });
  }
}
