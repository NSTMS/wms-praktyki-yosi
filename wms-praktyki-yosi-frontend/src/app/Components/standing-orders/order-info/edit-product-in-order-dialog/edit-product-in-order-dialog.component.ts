import { Component,Inject,Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { sendItemInOrder } from '@static/types/orderTypes';
@Component({
  selector: 'app-edit-product-in-order-dialog',
  templateUrl: './edit-product-in-order-dialog.component.html',
  styleUrls: ['./edit-product-in-order-dialog.component.scss']
})
export class EditProductInOrderDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<EditProductInOrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: sendItemInOrder,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

 
}
