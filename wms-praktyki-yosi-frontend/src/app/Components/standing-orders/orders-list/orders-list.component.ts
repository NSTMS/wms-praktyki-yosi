import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { OrdersService } from '@app/Services/fetching-services/orders.service';
import { returnedOrder } from '@static/types/orderTypes';
import { EditOrderDialogComponent } from './edit-order-dialog/edit-order-dialog.component';
import { MatDialog } from '@angular/material/dialog';
@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.scss']
})
export class OrdersListComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;
  options : string[]
  formGroup : FormGroup;
  dataSource: MatTableDataSource<returnedOrder> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  data : returnedOrder[] = []
  constructor(private _service: OrdersService, private router: Router,    public dialog: MatDialog) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin') this.router.navigate(['/table']);
    this.canAddAndDel = localStorage.getItem('role') != 'User';

    this.displayedColumns = [...this._service.listColumns];
    this.options = [...this._service.listColumns];
        
    this.formGroup = new FormGroup({
      'search' : new FormControl(''),
      'column' : new FormControl(''),
      'descending' : new FormControl(false)
    })

  
  }
  async loadData() {
    const searchTerm = `${this.formGroup.value.search}&orderBy=${this.formGroup.value.column}&descending=${this.formGroup.value.descending}` 
    this.data = await this._service.GetAll(searchTerm) as returnedOrder[]
    
    if (this.data != null) this.length = this.data.length || 0;
    else this.length = 0;

    this.dataSource = new MatTableDataSource(this.data);

    if (this.paginator != undefined) this.dataSource.paginator = this.paginator;
  }
  async ngAfterContentInit() {
    await this.loadData();
  }
  async handleDelete(guid: string) {
    await this._service.DeleteOrder(guid);
    window.location.reload();
  }
  handleEdit(guid : string)
  {
    const dialogData = this.data.filter(d=> d.id == guid)[0] as returnedOrder    
    const dialogRef = this.dialog.open(EditOrderDialogComponent, {
      data: {
        client:dialogData.client,
        interval: dialogData.interval,
        magazineId: dialogData.magazineId
      }
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })
    dialogRef.afterClosed().subscribe(result=> {
      this.data.map(async v =>{
        console.log(v);
        if(v.id == guid)
        {
          const res = {...dialogData, ...result}
          await this._service.UpdateOrder(guid, res);
          window.location.reload()
        }
      })
    });
  }






}
