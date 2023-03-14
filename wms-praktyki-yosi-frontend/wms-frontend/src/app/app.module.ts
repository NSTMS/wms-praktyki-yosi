import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DataTableComponent } from './data-table/data-table.component';
import { AppMaterialModule } from "./app.material-module";
import { EditFormComponent } from './edit-form/edit-form.component';
import {FormsModule} from "@angular/forms";
import { AddFormComponent } from './add-form/add-form.component'


@NgModule({
  declarations: [
    AppComponent,
    DataTableComponent,
    EditFormComponent,
    AddFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
