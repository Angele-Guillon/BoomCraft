import { Component,ViewChild} from '@angular/core';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { DataSource } from '@angular/cdk/collections';
import {NgForm, FormControl} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../_services/index';
import { User, Potion } from '../_models/index';
import { Jsonp } from '@angular/http';
import { DemandeService } from '../_services/demande.service';
import { PotionService } from '../_services/potion.service';


@Component({
  selector: 'app-messagerie',
  templateUrl: './messagerie.component.html',
  styleUrls: ['./messagerie.component.css']
})

export class MessagerieComponent  {
  faction :string;
  currentUser: User;
  users: User[] = [];

  ressources : Ressource[] = [
    {id: 1, name: 'Wood'},
    {id: 2, name: 'Gold'},
    {id: 3, name: 'Food'},
    {id: 4, name: 'Stone'},
  ];

  numbers : Numbers[] = [
    {id:0, name: 0},
    {id: 1, name: 100},
    {id: 2, name: 1000},
    {id: 3, name: 10000},
  ];
 
  constructor(private userService: UserService,private http: HttpClient,private potionService: PotionService,private demandeService: DemandeService) {
      this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
      //console.log(this.currentUser);
      
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;

 
    model: any = [{}];
    demandes: Demande[] = [];

    Potions: Potion[]=[];
    potionArray=[];
    demandeArray=[];
    iFaction:number;
    private loading: boolean = false;
    //private searchField: FormControl;
  
    ngOnInit() {
      this.faction=this.currentUser.faction;
      if(this.faction=='light' || this.faction=='lumiere'){
        this.iFaction=1;
      }else{
        this.iFaction=2
      }
      this.demandeService.getALLDemandeTroupe();
      console.log(localStorage);
      this.demandeArray=JSON.parse(localStorage.getItem('demandeTroupe'));
      if(this.demandeArray!=null){
        this.demandeArray.forEach(demande => {
          console.log(demande);
          this.demandes.push(demande);
        });
      }

      this.potionService.getAllPotion(this.currentUser.globalId);
      this.potionArray=JSON.parse(localStorage.getItem('potion'));
      //console.log(JSON.parse(localStorage.getItem('potion')));
    if(this.potionArray!=null){
      this.potionArray.forEach(potion => {
        //console.log(potion);
        this.Potions.push(potion);
      });
    }
    }

  ask(){
    //console.log(this.model);
    return this.demandeService.postdemande(this.model,this.currentUser.globalId,this.iFaction);
    }

  use(){
    //console.log(this.model.potion);
    return this.potionService.usePotionbyId(this.currentUser.globalId,this.model.potion);
    
  }

  displayedColumns = ['id', 'nbunit', 'button'];
  
  dataSource = new MatTableDataSource<Demande>(this.demandes);
    ngAfterViewInit() {
      console.log(this.demandes);
      this.dataSource.paginator = this.paginator;
    }
  }


  export interface Info {
    nb: Text ;
    userId: Text;
  }
  export interface Ressource {
    name: string ;
    id: number ;
  }
  export interface Numbers {
    name: number ;
    id: number ;
  }


  