import { Component, OnInit, ElementRef, ViewChild} from '@angular/core';
import {DataSource} from '@angular/cdk/collections';
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/startWith';
import 'rxjs/add/observable/merge';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/observable/fromEvent';
import { User } from '../_models/index';
import { UserService } from '../_services/index';
import { Ressource } from '../_models/Ressource';
import { RessourceService } from '../_services/ressource.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})


export class DashboardComponent implements OnInit {

faction :string;
currentUser: User;
users: User[] = [];
Ressources:Ressource[]=[];
ressourceArray=[];


constructor(private userService: UserService,private ressourceService: RessourceService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    console.log(this.currentUser);
}



  ngOnInit() {
    
    this.faction=this.currentUser.faction;
    if(this.faction=="lumiere"){
      this.faction="light";
    }
    if(this.faction=="ombre"){
      this.faction="shadow";
    }
    console.log(this.currentUser.globalId);
    this.ressourceService.getAllRessourcebyUser(this.currentUser.id);
    console.log(localStorage);
    this.ressourceArray=JSON.parse(localStorage.getItem('ressourceUser'));
    console.log(localStorage.getItem('ressourceUser'));
      this.ressourceArray.forEach(ressource => {
        console.log(ressource);
        this.Ressources.push(ressource);
    });
    }
  
}

  