import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataTableComponent } from './data-table/data-table.component';
import { EditFormComponent } from './edit-form/edit-form.component';
import { AddFormComponent } from './add-form/add-form.component';
const routes: Routes = [
  {path:"table", component: DataTableComponent},
  {path:"edit/:id", component:EditFormComponent},
  {path:"add", component:AddFormComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})    
export class AppRoutingModule { }
