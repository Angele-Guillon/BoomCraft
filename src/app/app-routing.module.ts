import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthService }          from './auth.service';
import { DashboardComponent }   from './dashboard/dashboard.component';
import { ForgetComponent }      from './forget/forget.component';
import { LoginComponent } from './login/login.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import {MessagerieComponent} from './messagerie/messagerie.component';
import { ArmeeComponent } from './armee/armee.component';
import { AttackComponent } from './attack/attack.component';
import { DefenseComponent } from './defense/defense.component';
import { UserCurrentComponent } from './user-current/user-current.component';
import { SubscribeComponent } from './subscribe/subscribe.component';
import { AuthGuard } from './_guards/index';



const routes: Routes = [
{ path: '', component: LoginComponent },
{ path: 'dashboard', component: DashboardComponent,canActivate: [AuthGuard]},
{ path: 'forget', component: ForgetComponent  },
{ path: 'messagerie',component: MessagerieComponent ,canActivate: [AuthGuard]},
{ path: 'armee', component: ArmeeComponent ,canActivate: [AuthGuard]},
{ path: 'change-password',component: ChangePasswordComponent,canActivate: [AuthGuard]},
{ path: 'attack',component: AttackComponent ,canActivate: [AuthGuard]},
{ path: 'defense',component: DefenseComponent ,canActivate: [AuthGuard]},
{ path: 'user-current',component: UserCurrentComponent,canActivate: [AuthGuard]},
{ path: 'subscribe', component: SubscribeComponent}
];

//export const routing = RouterModule.forRoot(routes);

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ],
  providers: [AuthService],
  declarations: []
})
export class AppRoutingModule { }

