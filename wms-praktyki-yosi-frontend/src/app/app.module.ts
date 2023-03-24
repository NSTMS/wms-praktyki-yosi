import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppMaterialModule } from './Modules/app.material-module';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavigationComponent } from './Components/navigation/navigation.component';
import { CompomponentsModule } from './Modules/compomponents.module';
import { CommonModule } from '@angular/common';
import { InfoMagazineComponent } from './Components/magazines-pages/info-magazine/info-magazine.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from './Interceptors/error-interceptor.interceptor';
import { AddShelfComponent } from './Components/magazines-pages/add-shelf/add-shelf.component';

@NgModule({
  declarations: [AppComponent, NavigationComponent, InfoMagazineComponent, AddShelfComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CompomponentsModule,
    HttpClientModule,
    AppMaterialModule,
    FormsModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
