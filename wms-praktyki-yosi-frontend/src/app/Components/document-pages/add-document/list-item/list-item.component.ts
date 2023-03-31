import { Component,Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DataReaderService } from '@app/Services/fetching-services/data-reader.service';
import { listItem } from '@static/types/documentTypes';
import { product } from '@static/types/productTypes';

@Component({
  selector: 'app-list-item',
  templateUrl: './list-item.component.html',
  styleUrls: ['./list-item.component.scss']
})
export class ListItemComponent {
  options : string[] = []
  autocomplete = new FormControl('');
  @Input() item:listItem = {} as listItem;

  @Input() deleteFunc = (id : number) =>{} 
  
  @Input() handleChange = (id : number, data: listItem) =>{}

  constructor(private _service : DataReaderService){
    this.loadData()
  }
  async loadData(){
    this.options = []
    const data = await this._service.GetAll("") as product[]
    this.options = data.map(p => p.productName)
  }
  handleSelect(){
    this.item.productName = this.autocomplete.value || "";
  }
  
  handleIChange(){
    this.handleChange(this.item.id, this.item)
  }
}

