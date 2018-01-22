import { Component,ViewChild} from '@angular/core';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { DataSource } from '@angular/cdk/collections';
import {NgForm, FormControl} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { UserService } from '../_services/index';
import { User } from '../_models/index';
import { Jsonp } from '@angular/http';
import { DemandeService } from '../_services/demande.service';


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
    {id: 1, name: 100},
    {id: 2, name: 1000},
    {id: 3, name: 10000},
  ];
 
  constructor(private userService: UserService,private http: HttpClient,private demande: DemandeService) {
      this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
      console.log(this.currentUser);
      
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;

 
    model: any = {};
    //jsonp: Jsonp;
    demandes: Promise<Demande[]>;

    private loading: boolean = false;
    private results: Observable<Demande[]>;
    private searchField: FormControl;
  
    ngOnInit() {
      this.faction=this.currentUser.faction;
      this.demandes=this.http.get<Array<Demande>>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/demande/params?:'+this.faction).toPromise();
      console.log(this.demandes);
    }

  ask(){
    // Make the HTTP request:
    console.log(this.model);
    const data_info : Info[]=[
      {nb:this.model.qty,userId:this.currentUser.id_global}
    ];
    console.log(data_info);
    this.http.post('/api/ask/',data_info);
  }

  displayedColumns = ['id', 'nbunit', 'button'];
  dataSource = new MatTableDataSource<Demande>(ELEMENT_DATA);
    ngAfterViewInit() {
      this.dataSource.paginator = this.paginator;
    }
  }

  export interface Element {
    name: string ;
    id: number ;
    date: number;
    text: string;
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


  const ELEMENT_DATA: Demande[] = [
    {id: 1, name: 'Hydrogen', date: 1, text: 'H'},
    {id: 2, name: 'Helium', date: 4, text: 'He'},
    {id: 3, name: 'Lithium', date: 6, text: 'Li'},
    {id: 4, name: 'Beryllium', date: 9, text: 'Be'},
    {id: 5, name: 'Boron', date: 10, text: 'B'},
    {id: 6, name: 'Carbon', date: 12, text: 'C'},
    {id: 7, name: 'Nitrogen', date: 14, text: 'N'},
    {id: 8, name: 'Oxygen', date: 15, text: 'O'},
    {id: 9, name: 'Fluorine', date: 18, text: 'F'},
    {id: 10, name: 'Neon', date: 20, text: 'Ne'},
    {id: 11, name: 'Sodium', date: 22, text: 'Na'},
    {id: 12, name: 'Magnesium', date: 24, text: 'Mg'},
    {id: 13, name: 'Aluminum', date: 26, text: 'Al'},
    {id: 14, name: 'Silicon', date: 28, text: 'Si'},
    {id: 15, name: 'Phosphorus', date: 30, text: 'P'},
    {id: 16, name: 'Sulfur', date: 32, text: 'S'},
    {id: 17, name: 'Chlorine', date: 35, text: 'Cl'},
    {id: 18, name: 'Argon', date: 39, text: 'Ar'},
    {id: 19, name: 'Potassium', date: 39, text: 'K'},
    {id: 20, name: 'Calcium', date: 40, text: 'Ca'},
  ];