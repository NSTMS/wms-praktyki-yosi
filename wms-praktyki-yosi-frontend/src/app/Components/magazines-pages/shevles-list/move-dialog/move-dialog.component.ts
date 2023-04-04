import { Component,Inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-move-dialog',
  templateUrl: './move-dialog.component.html',
  styleUrls: ['./move-dialog.component.scss']
})
export class MoveDialogComponent {

  destination = new FormControl(this.data, [
    Validators.required,
    Validators.pattern('^[A-Z]+[0-9]+/[0-9]+$'),
  ]);
  
  constructor(
    public dialogRef: MatDialogRef<MoveDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string,
  ) {}

  onNoClick(): void {
    if(this.destination.valid)
      this.dialogRef.close();
  }
  handleInput()
  {
    if(this.destination.valid)
      this.data = this.destination.value || ""
  }
}
