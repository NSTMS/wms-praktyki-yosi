import { Component } from '@angular/core';
import { ListItemComponent } from './list-item/list-item.component';
import { listItem,newDocument } from '@static/types/documentTypes';
@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss'],
})
export class AddDocumentComponent {

  data: newDocument ={
    date  : "",
    client : "",
    magazineId : -1,
    itemList :[]
  }
  constructor() {
  }
}

