import {  Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource,} from '@angular/material/table';
import { Router } from '@angular/router';
import { DocumentsService } from '@app/Services/fetching-services/documents.service';
import { catchError,tap,throwError ,map} from 'rxjs';

import { document } from '@static/types/documentTypes';
@Component({
  selector: 'app-documents-list',
  templateUrl: './documents-list.component.html',
  styleUrls: ['./documents-list.component.scss']
})
export class DocumentsListComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<document> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  constructor(private _service: DocumentsService, private router: Router) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);

    this.canAddAndDel = localStorage.getItem('role') != 'User';

    if (this.canAddAndDel)
      this.displayedColumns = [
        ...this._service.columns,
        'edit',
        'info',
        'delete',
      ];
    else this.displayedColumns = [...this._service.columns];
  }
  
  loadData() {
    return this._service.GetAll().subscribe((data) => {
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
  markAsFinished(guid : string,finished: boolean){
     return this._service.MarkAsFinished(guid,finished).pipe(
      map((res)=>{
        console.log(res);
        window.location.reload();

        return;
      })
     ).subscribe()
    }

}
