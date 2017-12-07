import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { PatternValidator } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AppRoutingModule }     from './app-routing.module';
import { AppComponent } from './app.component';

import {
  MatInputModule,
  MatButtonModule,
  MatIconRegistry,
  MatToolbarModule,
  MatMenuModule,
  MatTabsModule,
  MatPaginatorModule,
  MatTableModule,
  MatIconModule,
  MatGridListModule
} from '@angular/material';

import { DomSanitizer } from '@angular/platform-browser';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ForgetComponent } from './forget/forget.component';
import { LoginComponent } from './login/login.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { MessagerieComponent } from './messagerie/messagerie.component';
import { ArmeeComponent } from './armee/armee.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ForgetComponent,
    LoginComponent,
    ChangePasswordComponent,
    MessagerieComponent,
    ArmeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatMenuModule,
    MatTabsModule,
    MatGridListModule,
    BrowserAnimationsModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    HttpModule,
    MatTableModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { 

  constructor(mdIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer){
    mdIconRegistry.addSvgIconSet(domSanitizer.bypassSecurityTrustResourceUrl('./assets/mdi.svg'));
}
}
