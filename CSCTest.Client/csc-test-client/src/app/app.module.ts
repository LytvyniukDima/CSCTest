import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { TreeComponent } from './tree/tree.component';
import { TreeService } from './tree.service';

import { DxTreeViewModule } from 'devextreme-angular';
import { UrlBuilderService } from './url-builder.service';

@NgModule({
  declarations: [
    AppComponent,
    TreeComponent,
  ],
  imports: [
    BrowserModule,
    DxTreeViewModule,
    HttpClientModule
  ],
  providers: [
    TreeService,
    UrlBuilderService    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
