import { BrowserModule } from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { PatternValidator } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { HttpModule, JsonpModule, Jsonp, Response } from '@angular/http';
import { AppRoutingModule }     from './app-routing.module';
import { AppComponent } from './app.component';
import {AuthenticationService} from './_services/authentication.service';
import { AlertService } from './_services/alert.service';
import { UserService } from './_services/user.service';
import { AuthGuard } from './_guards/auth.guard';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
  MatStepperModule,
  MatIconRegistry,
} from '@angular/material';

import { DomSanitizer } from '@angular/platform-browser';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ForgetComponent } from './forget/forget.component';
import { LoginComponent } from './login/login.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { MessagerieComponent } from './messagerie/messagerie.component';
import { ArmeeComponent } from './armee/armee.component';
import { HttpClientModule , HTTP_INTERCEPTORS} from '@angular/common/http';
import { UserCurrentComponent } from './user-current/user-current.component';
import { AttackComponent } from './attack/attack.component';
import { DefenseComponent } from './defense/defense.component';
import { SubscribeComponent } from './subscribe/subscribe.component';
import { NavbarComponent } from './navbar/navbar.component';
import {AlertComponent} from './_directives/alert.component';
import { NavRessourceComponent } from './nav-ressource/nav-ressource.component';
import { JwtInterceptor } from './_helpers/index';
import { DemandeService } from './_services/demande.service';
import { PotionService } from './_services/potion.service';
import { RessourceService } from './_services/ressource.service';


@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ForgetComponent,
    LoginComponent,
    ChangePasswordComponent,
    MessagerieComponent,
    ArmeeComponent,
    UserCurrentComponent,
    AttackComponent,
    DefenseComponent,
    SubscribeComponent,
    NavbarComponent,
    AlertComponent,
    NavRessourceComponent
  ],
  imports: [
    BrowserModule,
    JsonpModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpModule,
    MatTableModule,
    MatPaginatorModule,
    MatToolbarModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatStepperModule,
    MatIconModule
  ],
  providers: [AuthenticationService,
    AlertService,
    AuthGuard,
    UserService,
    PotionService,
    RessourceService,
    DemandeService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 

  constructor(mdIconRegistry: MatIconRegistry, domSanitizer: DomSanitizer){
    mdIconRegistry.addSvgIconSet(domSanitizer.bypassSecurityTrustResourceUrl('./assets/mdi.svg'));
}
}
