import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { NavigationExtras, Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public authService: AuthService, public router: Router) {

  }
 

  connect(login, pass) {

    if (login == "test" && pass == "test" ) {

      let redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/dashboard';

      this.router.navigate([redirect]);
    }

  }

  logout() {

  }
  ngOnInit() {
  }

}
