import { Component,Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { orderToSend } from '@static/types/orderTypes';

@Component({
  selector: 'app-edit-order-dialog',
  templateUrl: './edit-order-dialog.component.html',
  styleUrls: ['./edit-order-dialog.component.scss']
})
export class EditOrderDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<EditOrderDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: orderToSend,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}
