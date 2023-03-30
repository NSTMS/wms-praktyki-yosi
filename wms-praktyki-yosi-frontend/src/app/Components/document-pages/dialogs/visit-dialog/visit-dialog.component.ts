import { Component,Inject} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { documentItem } from '@static/types/documentTypes';

@Component({
  selector: 'app-visit-dialog',
  templateUrl: './visit-dialog.component.html',
  styleUrls: ['./visit-dialog.component.scss'],
})
export class VisitDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<VisitDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: documentItem,
  ) {
    console.log(this.data);
    
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  
}
