import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { Router } from '@angular/router';
import { UserService, AlertService } from '../_services/index';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent implements OnInit {

  factions: Factions[] = [
    {id: 1, name: 'Shadow'},
    {id: 2, name: 'Light'},
  ];
  
  model: any = {};
  loading = false;

  constructor(
      private router: Router,
      private userService: UserService,
      private alertService: AlertService) { }

  register() {
      this.loading = true;
      this.userService.create(this.model)
          .subscribe(
              data => {
                  this.alertService.success('Registration successful', true);
                  this.router.navigate(['/login']);
              },
              error => {
                  this.alertService.error(error);
                  this.loading = false;
              });
  }
  ngOnInit() {}

}

export interface Factions {
  name: string ;
  id: number ;
}

 