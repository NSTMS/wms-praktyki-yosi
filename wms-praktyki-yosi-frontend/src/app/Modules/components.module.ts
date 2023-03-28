import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddFormComponent } from '@components/product-pages/add-form/add-form.component';
import { LoginFormComponent } from '@components/user-pages/login-form/login-form.component';
import { RegisterFormComponent } from '@components/user-pages/register-form/register-form.component';
import { UsersTableComponent } from '@components/user-pages/users-table/users-table.component';
import { UsersEditComponent } from '@components/user-pages/users-edit/users-edit.component';
import { ProductInfoComponent } from '@components/product-pages/product-info/product-info.component';
import { EditLocationComponent } from '@components/location-pages/edit-location/edit-location.component';
import { AddLocationComponent } from '@components/location-pages/add-location/add-location.component';
import { DataTableComponent } from '@components/product-pages/data-table/data-table.component';
import { EditFormComponent } from '@components/product-pages/edit-form/edit-form.component';
import { AppMaterialModule } from './app.material-module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from '@app/app-routing.module';
import { AddMagazineComponent } from '@app/Components/magazines-pages/add-magazine/add-magazine.component';
import { EditMagazineComponent } from '@app/Components/magazines-pages/edit-magazine/edit-magazine.component';
import { MagazineListComponent } from '@app/Components/magazines-pages/magazine-list/magazine-list.component';
import { AddShelfComponent } from '@app/Components/magazines-pages/add-shelf/add-shelf.component';
import { AddDocumentComponent } from '@app/Components/document-pages/add-document/add-document.component';
import { InfoMagazineComponent } from '@app/Components/magazines-pages/info-magazine/info-magazine.component';
import { EditDocumentComponent } from '@app/Components/document-pages/edit-document/edit-document.component';
import { InfoDocumentComponent } from '@app/Components/document-pages/info-document/info-document.component';
import { DocumentsListComponent } from '@app/Components/document-pages/documents-list/documents-list.component';
import { DocumentsInMagazineComponent } from '@app/Components/document-pages/documents-in-magazine/documents-in-magazine.component';

@NgModule({
  declarations: [
    DataTableComponent,
    EditFormComponent,
    AddFormComponent,
    LoginFormComponent,
    RegisterFormComponent,
    UsersTableComponent,
    UsersEditComponent,
    ProductInfoComponent,
    EditLocationComponent,
    AddLocationComponent,
    AddMagazineComponent,
    EditMagazineComponent,
    MagazineListComponent,
    AddShelfComponent,
    AddDocumentComponent,
    InfoMagazineComponent,
    EditDocumentComponent,
    InfoDocumentComponent,
    DocumentsListComponent,
    DocumentsInMagazineComponent
  ],
  imports: [
    CommonModule,
    AppMaterialModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
  ],
})
export class ComponentsModule {}
