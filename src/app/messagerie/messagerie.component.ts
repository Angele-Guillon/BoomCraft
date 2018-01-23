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
 
  constructor(private userService: UserService,private http: HttpClient,private potionService: PotionService,private demande: DemandeService) {
      this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
      console.log(this.currentUser);
      
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;

 
    model: any = [{}];
    //jsonp: Jsonp;
    demandes: Promise<Demande[]>;

    Potions: Potion[]=[];
    potionArray=[];
    private loading: boolean = false;
    private searchField: FormControl;
  
    ngOnInit() {
      this.faction=this.currentUser.faction;
      //this.demandes=this.http.get<Array<Demande>>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/demande/params?:'+this.faction).toPromise();
      this.potionService.getAllPotion(this.currentUser.globalId);
      this.potionArray=JSON.parse(localStorage.getItem('potion'));
      console.log(JSON.parse(localStorage.getItem('potion')));
      if(this.potionArray!=null){
      this.potionArray.forEach(potion => {
        console.log(potion);
        this.Potions.push(potion);
      });
    }
    }

  ask(){
    // Make the HTTP request:
    //console.log(this.model);
    
    this.http.post('/api/ask/',JSON.parse(this.model));
  }

  use(){
    console.log(this.currentUser.globalId);
    return this.potionService.usePotionbyId(this.currentUser.globalId,this.model.potion);
    
  }

  displayedColumns = ['id', 'nbunit', 'button'];
  dataSource = new MatTableDataSource<Demande>(Demande_DATA);
    ngAfterViewInit() {
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


  const Demande_DATA: Demande[] = [
    {id: 1, nbUnit: 1},
    {id: 2, nbUnit: 1},
    {id: 3, nbUnit: 1},
    {id: 4, nbUnit: 1},
    {id: 5, nbUnit: 1},
    {id: 6, nbUnit: 1},
    {id: 7, nbUnit: 1},
    {id: 8, nbUnit: 1},
    {id: 78, nbUnit: 300},
    {id: 300, nbUnit: 400},
    {id: 34, nbUnit: 500},
    {id: 23, nbUnit: 600},
    {id: 24, nbUnit: 700},
    {id: 56, nbUnit: 800},
    {id: 20, nbUnit: 1222},
    {id: 70, nbUnit: 1112},
    {id: 80, nbUnit: 1},
    {id: 67, nbUnit: 1},
    {id: 89, nbUnit: 1},
    {id: 90, nbUnit: 1},
    {id: 58, nbUnit: 1},
    {id: 60, nbUnit: 1},
    {id: 50, nbUnit: 1},
    {id: 30, nbUnit: 1},
    {id: 42, nbUnit: 1},

  ];