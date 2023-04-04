import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DataTableComponent } from './Components/product-pages/data-table/data-table.component';
import { EditFormComponent } from './Components/product-pages/edit-form/edit-form.component';
import { AddFormComponent } from './Components/product-pages/add-form/add-form.component';
import { LoginFormComponent } from './Components/user-pages/login-form/login-form.component';
import { RegisterFormComponent } from './Components/user-pages/register-form/register-form.component';
import { UsersTableComponent } from './Components/user-pages/users-table/users-table.component';
import { UsersEditComponent } from './Components/user-pages/users-edit/users-edit.component';
import { ProductInfoComponent } from './Components/product-pages/product-info/product-info.component';
import { EditLocationComponent } from './Components/location-pages/edit-location/edit-location.component';
import { AddLocationComponent } from './Components/location-pages/add-location/add-location.component';
import { MagazineListComponent } from './Components/magazines-pages/magazine-list/magazine-list.component';
import { AddMagazineComponent } from './Components/magazines-pages/add-magazine/add-magazine.component';
import { EditMagazineComponent } from './Components/magazines-pages/edit-magazine/edit-magazine.component';
import { InfoMagazineComponent } from './Components/magazines-pages/info-magazine/info-magazine.component';
import { AddShelfComponent } from './Components/magazines-pages/add-shelf/add-shelf.component';
import { AddDocumentComponent } from './Components/document-pages/add-document/add-document.component';
import { InfoDocumentComponent } from './Components/document-pages/info-document/info-document.component';
import { DocumentsListComponent } from './Components/document-pages/documents-list/documents-list.component';
import { ShevlesListComponent } from './Components/magazines-pages/shevles-list/shevles-list.component';
import { ShelfDetailComponent } from './Components/magazines-pages/shelf-detail/shelf-detail.component';
import { OrdersListComponent } from '@app/Components/standing-orders/orders-list/orders-list.component';
import { AddOrderComponent } from '@app/Components/standing-orders/add-order/add-order.component';
import { OrderInfoComponent } from './Components/standing-orders/order-info/order-info.component';

const routes: Routes = [
  { path: '', redirectTo: '/table', pathMatch: 'full' },
  { path: 'table', component: DataTableComponent },
  { path: 'edit/:id', component: EditFormComponent },
  { path: 'info/:id', component: ProductInfoComponent },
  { path: 'locations/edit/:id', component: EditLocationComponent },
  { path: 'locations/add', component: AddLocationComponent },
  { path: 'magazines/add', component: AddMagazineComponent },
  { path: 'magazines/addShelf', component: AddShelfComponent },
  { path: 'magazines/edit/:id', component: EditMagazineComponent },
  { path: 'magazines/info/:id', component: InfoMagazineComponent },
  { path: 'magazines/info/:id/shelves', component: ShevlesListComponent},
  { path: 'magazines/info/:id/shelves/:position', component: ShelfDetailComponent},
  {
    path: 'magazines/info/:magazineId/product/:id',
    component: ProductInfoComponent,
  },
  { path: 'magazines', component: MagazineListComponent },
  { path: 'add', component: AddFormComponent },
  { path: 'login', component: LoginFormComponent },
  { path: 'register', component: RegisterFormComponent },
  { path: 'users', component: UsersTableComponent },
  { path: 'users/edit/:id', component: UsersEditComponent },
  { path: 'documents', component: DocumentsListComponent },
  { path: 'documents/add', component: AddDocumentComponent },
  {
    path: 'documents/info/:id',
    pathMatch: 'full',
    component: InfoDocumentComponent,
  },
  { path: 'orders', component: OrdersListComponent },
  { path: 'orders/add', component: AddOrderComponent },
  { path: 'orders/:id', component: OrderInfoComponent },
  
  { path: '**', redirectTo: '/table' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
