import { Component,Inject } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DataReaderService } from '@app/Services/fetching-services/data-reader.service';
import { DocumentsService } from '@app/Services/fetching-services/documents.service';
import { product } from '@static/types/productTypes';

@Component({
  selector: 'app-add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.scss']
})
export class AddDialogComponent {
  options : string[] = []
  formGroup: FormGroup;

  constructor(
    private _service: DocumentsService,
    private _reader : DataReaderService,
    public dialogRef: MatDialogRef<AddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data:{
      guid: string;
      item:{
        productName : string,
        arriving : boolean,
        quantity : number,
        tag : string
      }
    },
    ){
      this.loadData()
      this.formGroup = new FormGroup({
        'arriving' : new FormControl(false),
        'autocomplete' : new FormControl(''),
        'quantity' : new FormControl(''),
        'tag' : new FormControl('')
      })
    }
    async loadData(){
      this.options = []
      const data = await this._reader.GetAll("") as product[]
      this.options = data.map(p => p.productName)
    }
    async handleSumbit(){
      if(this.formGroup.invalid)
        return;
      console.log(this.formGroup.value);
      
      this.data.item = {
        productName : this.formGroup.value.autocomplete,
        arriving :  this.formGroup.value.arriving,
        quantity :  this.formGroup.value.quantity,
        tag :  this.formGroup.value.tag
      }
      console.log(this.data.item);
      
      this._service.AddItem(this.data.guid,this.data.item)
      this.dialogRef.close()
      window.location.reload()
    }
}
