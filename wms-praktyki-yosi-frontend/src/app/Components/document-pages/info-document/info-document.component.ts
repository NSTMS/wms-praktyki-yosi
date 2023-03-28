import {  Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource,} from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DocumentsService } from '@app/Services/fetching-services/documents.service';
import { catchError,tap,throwError ,map} from 'rxjs';
import { document,documentItem} from '@static/types/documentTypes';

@Component({
  selector: 'app-info-document',
  templateUrl: './info-document.component.html',
  styleUrls: ['./info-document.component.scss']
})
export class InfoDocumentComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;
  id : string = "";
  dataSource: MatTableDataSource<documentItem> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  columns : string[] = ['id','productName','position','quantityPlanned','quantityDone','status','tag','arriving'] 
  constructor(private _service: DocumentsService, private router: Router, private route : ActivatedRoute) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);

    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [
        ...this.columns,
        'edit',
        'delete',
      ];
    else this.displayedColumns = [...this.columns];
  }
  
  loadData() {
    this.id = this.route.snapshot.params['id'];
    return this._service.GetById(this.id).subscribe((data) => {
       if (data != null) 
         this.length = data.length 
       this.dataSource = new MatTableDataSource(data.items);
 
       if (this.paginator != undefined)
         this.dataSource.paginator = this.paginator;
       });
   }
   
  ngAfterContentInit() {
    this.loadData();
  }
    handleDelete(guid : string) {
    return this._service.Delete(guid).pipe(
      tap(() => {
          window.location.reload();
      }),
      catchError((error) => {
        return throwError(() => new Error(error));
      })
    ).subscribe();
  }



}
