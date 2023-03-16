import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataTableComponent } from './Components/data-table/data-table.component';
import { EditFormComponent } from './Components/edit-form/edit-form.component';
import { AddFormComponent } from './Components/add-form/add-form.component';
import { LoginFormComponent } from './Components/login-form/login-form.component';
import { RegisterFormComponent } from './Components/register-form/register-form.component';
const routes: Routes = [
  {path: "", redirectTo:"/table", pathMatch: 'full'},
  {path:"table", component: DataTableComponent},
  {path:"edit/:id", component:EditFormComponent},
  {path:"add", component:AddFormComponent},
  {path:"login",component:LoginFormComponent},
  {path:"register",component:RegisterFormComponent},
  {path:"**",redirectTo:"/table"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})    
export class AppRoutingModule { }
