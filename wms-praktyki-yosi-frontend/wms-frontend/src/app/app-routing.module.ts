import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataTableComponent } from './Components/data-table/data-table.component';
import { EditFormComponent } from './Components/edit-form/edit-form.component';
import { AddFormComponent } from './Components/add-form/add-form.component';
import { LoginFormComponent } from './Components/login-form/login-form.component';
import { RegisterFormComponent } from './Components/register-form/register-form.component';
import { UsersTableComponent } from './Components/users-table/users-table.component';
import { UsersEditComponent } from './Components/users-edit/users-edit.component';
import { ProductInfoComponent } from './Components/product-info/product-info.component';
import { EditLocationComponent } from './Components/edit-location/edit-location.component';
import { AddLocationComponent } from './Components/add-location/add-location.component';


const routes: Routes = [
  {path: "", redirectTo:"/table", pathMatch: 'full'},
  {path:"table", component: DataTableComponent},
  {path:"edit/:id", component:EditFormComponent},
  {path:"info/:id", component:ProductInfoComponent},
  {path:"locations/edit/:id", component:EditLocationComponent},
  {path:"locations/add", component:AddLocationComponent},
  {path:"add", component:AddFormComponent},
  {path:"login",component:LoginFormComponent},
  {path:"register",component:RegisterFormComponent},
  {path:"users", component:UsersTableComponent},
  {path:"users/edit/:id", component:UsersEditComponent},
  {path:"**",redirectTo:"/table"},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
