import {NgModule} from '@angular/core';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatInputModule} from '@angular/material/input';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatSortModule} from '@angular/material/sort';
import {MatButtonModule} from '@angular/material/button';
import {MatGridListModule} from "@angular/material/grid-list"
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
@NgModule({
// since we're exporting these modules, add them to export
  exports: [
      MatTableModule,
      MatSortModule,
      MatProgressSpinnerModule,
      MatInputModule,
      MatPaginatorModule,
      MatButtonModule,
      MatGridListModule,
      MatToolbarModule,
      MatIconModule,
      MatMenuModule
  ]
})
export class AppMaterialModule {}
