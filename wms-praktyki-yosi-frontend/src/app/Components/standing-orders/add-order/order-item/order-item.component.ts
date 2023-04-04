import { ListKeyManager } from '@angular/cdk/a11y';
import { Component,Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { DataReaderService } from '@app/Services/fetching-services/data-reader.service';
import { listItem } from '@static/types/documentTypes';
import { orderItem } from '@static/types/orderTypes';
import { product } from '@static/types/productTypes';

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.scss']
})
export class OrderItemComponent {
  options : string[] = []
  autocomplete = new FormControl('');
  @Input() item:orderItem = {} as orderItem;

  @Input() deleteFunc = (id : number) =>{} 
  
  @Input() handleChange = (id : number, data: orderItem) =>{}

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
