import { Component, ViewChild, Inject } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentsService } from '@app/Services/fetching-services/documents.service';
import { detailedDocument, documentItem,EditDialogData, ItemToSend, visitedElement } from '@static/types/documentTypes';
import {
  MatDialog,
} from '@angular/material/dialog';
import { EditDialogComponent } from '../dialogs/edit-dialog/edit-dialog.component';
import { VisitDialogComponent } from '../dialogs/visit-dialog/visit-dialog.component';
import { AddDialogComponent } from '../dialogs/add-dialog/add-dialog.component';
import { PutbackDialogComponent } from '../dialogs/putback-dialog/putback-dialog.component';

@Component({
  selector: 'app-info-document',
  templateUrl: './info-document.component.html',
  styleUrls: ['./info-document.component.scss'],
})
export class InfoDocumentComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;
  id: string = '';
  dataSource: MatTableDataSource<documentItem> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  data: detailedDocument = {
    id: '',
    date: '',
    magazineId: -1,
    client: '',
    totalQuantity: -1,
    quantityDone: -1,
    finished: false,
    items: [],
  };
  columns: string[] = [
    'id',
    'productName',
    'position',
    'quantityPlaned',
    'quantityDone',
    'status',
    'tag',
    'arriving',
  ];
  constructor(
    private _service: DocumentsService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog
  ) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') == 'User') this.router.navigate(['/table']);
    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [...this.columns, 'visit','putback', 'edit', 'delete'];
    else this.displayedColumns = [...this.columns];
  }

  async loadData() {
    this.id = this.route.snapshot.params['id'];
    const data = await this._service.GetById(this.id)    
    this.data = data as detailedDocument;
    this.data.id = this.id;
    this.dataSource = new MatTableDataSource(this.data.items);

    if (this.paginator != undefined) this.dataSource.paginator = this.paginator;
  }

  ngAfterContentInit() {
    this.loadData();
  }
  async handleDelete(guid: string) {
    const dat = this.data.items.filter(d=> d.id == guid)[0] as documentItem    
    await this._service.DeleteElement(this.id, dat.id);
    window.location.reload();
  }
  async handleEdit(guid: string) {

    const dialogData = this.data.items.filter(d=> d.id == guid)[0] as documentItem    
    const dialogRef = this.dialog.open(EditDialogComponent, {
      data: {
        arriving:dialogData.arriving,
        quantity: dialogData.quantityPlaned,
        tag: dialogData.tag
      }
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })

    dialogRef.afterClosed().subscribe(result=> {
      this.data.items.map(async v =>{
        console.log(v);
        
        if(v.id == guid)
        {
          const res = result as EditDialogData
          await this._service.UpdateItem(this.id, v.productId, res);
          window.location.reload()
          
        }
      })
    });
  }
  async addNewItemToDoc()
  {
    const dialogRef = this.dialog.open(AddDialogComponent, {
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
  }

  handleVisit(guid: string)  {
    const dialogData = this.data.items.filter(d=> d.id == guid)[0] as documentItem
    const dialogRef = this.dialog.open(VisitDialogComponent, {
      data: {...dialogData}
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })
    dialogRef.afterClosed().subscribe(result=> {
      this.data.items.map(async v =>{      
        if(v.id == guid)
        {
          const res = 
          {
            productName : result.productName,
            position : result.position,
            quantity : result.quantityDone,
            tag : result.tag
          }
    
          await this._service.VisitLocation(this.id, res);
          window.location.reload()
        }
      })
    });
  }
  handlePutBack(guid: string){
    const dialogData = this.data.items.filter(d=> d.id == guid)[0] as documentItem
    const dialogRef = this.dialog.open(PutbackDialogComponent, {
      data: {...dialogData}
    })
    dialogRef.backdropClick().subscribe(()  =>{
      window.location.reload()
    })
    dialogRef.afterClosed().subscribe(result=> {     
      this.data.items.map(async v =>{      
        if(v.id == guid)
        {
          const res = 
          {
            productName : result.productName,
            position : result.position,
            quantity : result.quantityDone,
            tag : result.tag
          }          
          await this._service.PutBackToLocation(this.id, res);
          window.location.reload()
        }
      })
    });
  }
}
