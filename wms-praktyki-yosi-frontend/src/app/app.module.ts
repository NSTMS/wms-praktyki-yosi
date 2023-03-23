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

@NgModule({
  declarations: [AppComponent, NavigationComponent, InfoMagazineComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CompomponentsModule,
    AppMaterialModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
