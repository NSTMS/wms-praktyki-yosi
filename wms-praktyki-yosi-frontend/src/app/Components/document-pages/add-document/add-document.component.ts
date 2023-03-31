import { Component } from '@angular/core';
import { listItem,newDocument,ItemToSend} from '@static/types/documentTypes';
import { Router } from '@angular/router';
declare var require: any;
const connection = require('static/connection.json');
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-add-document',
  templateUrl: './add-document.component.html',
  styleUrls: ['./add-document.component.scss'],
})
export class AddDocumentComponent {
  link: string = `${connection.protocole}://${connection.ip}:${connection.port}/api/documents`;

  data: newDocument ={
    date  : "",
    client : "",
    magazineId : 0,
    itemList :[]
  }
  counter : number = 0
  constructor(private router : Router,private http: HttpClient) {
    if (localStorage.getItem('token') == null) this.router.navigate(['/login']);
    if (localStorage.getItem('role') != 'Admin') this.router.navigate(['/table']);
  }
  handleAddItem(){
    this.data.itemList.push(
      {
        productName : '',
        arriving : false,
        quantity : 0,
        tag : "",
        id: ++this.counter
      }
    )
    
  }
  handleDeleteItem(id : number){
    this.data.itemList =this.data.itemList.filter(i => i.id !== id)
  }
  handleItemChange(id: number, data : listItem)
  {
    this.data.itemList.map( i =>{
      if(i.id == id)
      {
        return data
      }
      return i
    })
  }
  async handleSumbit()
  {
    let array : ItemToSend[] = []
    this.data.itemList.forEach( i =>{
      array.push({
        productName : i.productName,
        arriving : i.arriving,
        quantity : i.quantity,
        tag : i.tag
      })
    })
    await firstValueFrom(this.http.post(this.link, {
      ...this.data,
      itemList : array
    }))
    this.router.navigate(['/documents'])
  }
}

