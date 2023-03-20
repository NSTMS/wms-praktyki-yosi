import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppMaterialModule } from "./app.material-module";
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataTableComponent } from './Components/data-table/data-table.component';
import { EditFormComponent } from './Components/edit-form/edit-form.component';
import { ReactiveFormsModule } from "@angular/forms";
import { AddFormComponent } from './Components/add-form/add-form.component'
import { LoginFormComponent } from './Components/login-form/login-form.component';
import { RegisterFormComponent } from './Components/register-form/register-form.component';
import { UsersTableComponent } from './Components/users-table/users-table.component';
import { UsersEditComponent } from './Components/users-edit/users-edit.component';
import { NavigationComponent } from './Components/navigation/navigation.component';
import { ProductInfoComponent } from './Components/product-info/product-info.component';
import { EditLocationComponent } from './Components/edit-location/edit-location.component';

@NgModule({
  declarations: [
    AppComponent,
    DataTableComponent,
    EditFormComponent,
    AddFormComponent,
    LoginFormComponent,
    RegisterFormComponent,
    UsersTableComponent,
    UsersEditComponent,
    NavigationComponent,
    ProductInfoComponent,
    EditLocationComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    ReactiveFormsModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
