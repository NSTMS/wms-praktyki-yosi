import { Component,Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { documentItem } from '@static/types/documentTypes';

@Component({
  selector: 'app-putback-dialog',
  templateUrl: './putback-dialog.component.html',
  styleUrls: ['./putback-dialog.component.scss']
})
export class PutbackDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<PutbackDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: documentItem,
  ) {
    console.log(this.data);
    
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  
}