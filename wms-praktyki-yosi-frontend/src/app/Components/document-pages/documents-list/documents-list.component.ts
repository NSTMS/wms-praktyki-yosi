import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DocumentsService } from '@app/Services/fetching-services/documents.service';
import { document } from '@static/types/documentTypes';
@Component({
  selector: 'app-documents-list',
  templateUrl: './documents-list.component.html',
  styleUrls: ['./documents-list.component.scss'],
})
export class DocumentsListComponent {
  @ViewChild('paginator') paginator: MatPaginator | undefined;

  dataSource: MatTableDataSource<document> = new MatTableDataSource();
  displayedColumns: string[];
  length: number = 0;
  canAddAndDel: boolean;
  constructor(private _service: DocumentsService, private router: Router) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin') this.router.navigate(['/table']);

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

  async loadData() {
    const data = await this._service.GetAll();
    console.log(data);
    if (data != null) this.length = (data as document[]).length || 0;
    else this.length = 0;

    this.dataSource = new MatTableDataSource(data as document[]);

    if (this.paginator != undefined) this.dataSource.paginator = this.paginator;
  }

  async ngAfterContentInit() {
    await this.loadData();
  }
  async handleDelete(guid: string) {
    await this._service.Delete(guid);
    window.location.reload();
  }
  async markAsFinished(guid: string, finished: boolean) {
    await this._service.MarkAsFinished(guid, finished);
    window.location.reload();
  }
}
