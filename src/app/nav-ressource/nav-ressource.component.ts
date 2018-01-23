import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/index';
import { RessourceService } from '../_services/ressource.service';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';
import { Ressource } from '../_models/Ressource';

@Component({
  selector: 'app-nav-ressource',
  templateUrl: './nav-ressource.component.html',
  styleUrls: ['./nav-ressource.component.css']
})
export class NavRessourceComponent implements OnInit {
  currentUser: User;
  ressourceArray=[];
  Ressources:Ressource[]=[];
  constructor(private userService: UserService,private http: HttpClient,private ressourceService: RessourceService) {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
    console.log(this.currentUser);
    
}

  ngOnInit() {
    
    /*console.log(this.currentUser.globalId);
    this.ressourceService.getAllRessourcebyUser(this.currentUser.id);
    console.log(localStorage);
    this.ressourceArray=JSON.parse(localStorage.getItem('ressourceUser'));
    console.log(this.ressourceArray);
      this.ressourceArray.forEach(ressource => {
        console.log(ressource);
        this.Ressources.push(ressource);*/
    //}

  }

}
