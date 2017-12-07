import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthService }          from './auth.service';
import { DashboardComponent }   from './dashboard/dashboard.component';
import { ForgetComponent }      from './forget/forget.component';
import { LoginComponent } from './login/login.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import {MessagerieComponent} from './messagerie/messagerie.component';
import { ArmeeComponent } from './armee/armee.component';



const routes: Routes = [
{ path: '', component: LoginComponent },
{ path: 'dashboard', component: DashboardComponent },
{ path: 'forget', component: ForgetComponent },
{ path: 'messagerie',component: MessagerieComponent },
{ path: 'armee', component: ArmeeComponent },
{ path: 'change-password',component: ChangePasswordComponent}
];


@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ],
  providers: [AuthService],
  declarations: []
})
export class AppRoutingModule { }
