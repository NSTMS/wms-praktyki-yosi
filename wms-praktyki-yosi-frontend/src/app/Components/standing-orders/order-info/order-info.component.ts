import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { OrdersService } from '@app/Services/fetching-services/orders.service';
import { returnedOrder, orderItem ,returnedOrderWithProducts,sendItemInOrder} from '@static/types/orderTypes';
import { EditProductInOrderDialogComponent } from './edit-product-in-order-dialog/edit-product-in-order-dialog.component';
import { AddProductToOrderDialogComponent } from './add-product-to-order-dialog/add-product-to-order-dialog.component';

@Component({
  selector: 'app-order-info',
  templateUrl: './order-info.component.html',
  styleUrls: ['./order-info.component.scss']
})
export class OrderInfoComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;
  id: string = '';
  dataSource: MatTableDataSource<sendItemInOrder> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  data : returnedOrderWithProducts = {} as returnedOrderWithProducts
  columns: string[] = [
    'productName',
    'quantity',
    'tag',
    'arriving',
  ];
  constructor(
    private _service: OrdersService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);
    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [...this.columns, 'edit', 'delete'];
    else this.displayedColumns = [...this.columns];
  }
  async loadData() {
    this.id = this.route.snapshot.params['id'];
    const data = await this._service.GetById(this.id)    
    this.data = data as returnedOrderWithProducts;
    this.data.id = this.id;
    this.dataSource = new MatTableDataSource(this.data.items);
    
    if (this.paginator != undefined) this.dataSource.paginator = this.paginator;
  }

  ngAfterContentInit() {
    this.loadData();
  }
  addNewProduct(){
    const dialogRef = this.dialog.open(AddProductToOrderDialogComponent, {
      data: {
        guid: this.id,
        item: {
          productName : "",
          arriving : false,
          quantity : 0,
          tag : ""
        }
      }
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })
    
  }
  handleEdit(guid : string)
  {
    const dialogData = this.data.items?.filter(d=> d.id == guid)[0] as sendItemInOrder  
    console.log(dialogData);
    const dialogRef = this.dialog.open(EditProductInOrderDialogComponent, {
      data: dialogData
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })
    dialogRef.afterClosed().subscribe(result=> {
      this.data.items?.map(async v =>{
        console.log(v);
        if(v.id == guid)
        {
          const res = {...dialogData, ...result}
          console.log(res);
          
          await this._service.UpdateProductInOrder(this.id,guid, res);
          window.location.reload()
        }
      })
    });
  }
  async handleDelete(guid: string)
  {
    await this._service.DeleteProductFromOrder(this.id,guid);
    window.location.reload()
  }

}
