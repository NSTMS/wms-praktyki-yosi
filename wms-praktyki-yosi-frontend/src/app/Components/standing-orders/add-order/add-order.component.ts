import { Component } from '@angular/core';
import { order,orderItem, orderSendItem, orderToSend} from '@static/types/orderTypes';
import { Router } from '@angular/router';
declare var require: any;
const connection = require('static/connection.json');
import { OrdersService } from '@app/Services/fetching-services/orders.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.scss']
})
export class AddOrderComponent {

  formGroup = new FormGroup({
    date : new FormControl('', [
      Validators.required,
      Validators.pattern('^[0-9]*:([0-9]|1[0-9]|2[0-3]):([1-5]?[0-9]):([1-5]?[0-9])::([0-9]{0,3})$'),
    ]),
    magazineId :new FormControl(0, [Validators.required]) ,
    client : new FormControl("", [Validators.required])
  })

  data: order ={
    id : "",
    interval : 0,
    nextOrder : "", 
    client : "",
    magazineId : 0,
    orderItems:[]
  }
  counter : number = 0

  constructor(private router : Router,private _service : OrdersService) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin') this.router.navigate(['/table']);
  }
  handleAddItem(){
    this.data.orderItems.push(
      {
        id: ++this.counter,
        productName : "",
        arriving : false,
        quantity : 0,
        tag : ""
      } as orderItem
    )
  }

  handleDeleteItem(id : number){
    this.data.orderItems =this.data.orderItems.filter(i => i.id !== id)
  }
  handleItemChange(id: number, data : orderItem)
  {
    this.data.orderItems.map( i =>{
      if(i.id == id)
      {
        return data
      }
      return i
    })
  }
  async handleSumbit()
  {
    let array : orderSendItem[] = []
    this.data.orderItems.forEach( i =>{
      array.push({
        productName : i.productName,
        arriving : i.arriving,
        quantity : i.quantity,
        tag : i.tag
      })
    })
    let order : orderToSend ={
      interval : this.calcDate(this.formGroup.value.date as string),
      client : this.formGroup.value.client || "",
      magazineId : this.formGroup.value.magazineId || 0,
      items:array
    }

    await this._service.AddNewOrder(order)
    this.router.navigate(['/documents'])
  }


  private calcDate(input : string): number{
    let date = input.split(":").map(e => parseInt(e))
    let sum = date[0]*86400000 + date[1]*3600000 + date[2]*60000 + date[3]*1000 + date[5]
    return sum
  }

}
