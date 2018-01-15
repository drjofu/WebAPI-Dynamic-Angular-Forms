import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import {HttpClientModule} from '@angular/common/http'

import { AppComponent } from './app.component';
import { DynamicFormComponent } from './dynamic-forms/dynamic-form/dynamic-form.component';
import { DynamicFormPropertyComponent } from './dynamic-forms/dynamic-form-property/dynamic-form-property.component';
import { DynamicFormsModule } from './dynamic-forms/dynamic-forms.module';
import { ApiService } from './api.service';
import { AppRoutingModule } from './app-routing.module';
import { MainWindowComponent } from './main-window/main-window.component';
import { DataViewComponent } from './data-view/data-view.component';


@NgModule({
  declarations: [
    AppComponent,
    MainWindowComponent,
    DataViewComponent
  ],
  imports: [
     FormsModule,ReactiveFormsModule ,
    HttpModule, HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    DynamicFormsModule
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
