import { Component,ViewChild} from '@angular/core';
import {MatPaginator, MatTableDataSource } from '@angular/material';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { DataSource } from '@angular/cdk/collections';
import {NgForm} from '@angular/forms';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-messagerie',
  templateUrl: './messagerie.component.html',
  styleUrls: ['./messagerie.component.css']
})
export class MessagerieComponent  {


  @ViewChild(MatPaginator) paginator: MatPaginator;
  faction='light';
  // faction='shadows';

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

  //pris sur le net

    results: string[];
    

  // Inject HttpClient into your component or service.
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
      // Make the HTTP request:
    
    this.http.get('/api/demande/'+this.faction).subscribe(data => {
      // Read the result field from the JSON response.
     // data.forEach(element =>{
        
      });
     // this.results = data['results'];
    //});
  }

  ask(idR : number,qty:number ){
    // Make the HTTP request:
    const data_info : Info[]=[
      {id:idR,nb:qty,/*user*/}
    ]
    
    this.http.post('/api/ask/',data_info)
  }

  ///

  displayedColumns = ['id', 'nbunit', 'button'];
  dataSource = new MatTableDataSource<Element>(ELEMENT_DATA);
  //dataressources = new MatTableDataSource<Ressource>(ressources);
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
    nb: number ;
    id: number ;
  }

  export interface Ressource {
    name: string ;
    id: number ;
  }
  export interface Numbers {
    name: number ;
    id: number ;
  }


  const ELEMENT_DATA: Element[] = [
    {id: 1, name: 'Hydrogen', date: 1.0079, text: 'H'},
    {id: 2, name: 'Helium', date: 4.0026, text: 'He'},
    {id: 3, name: 'Lithium', date: 6.941, text: 'Li'},
    {id: 4, name: 'Beryllium', date: 9.0122, text: 'Be'},
    {id: 5, name: 'Boron', date: 10.811, text: 'B'},
    {id: 6, name: 'Carbon', date: 12.0107, text: 'C'},
    {id: 7, name: 'Nitrogen', date: 14.0067, text: 'N'},
    {id: 8, name: 'Oxygen', date: 15.9994, text: 'O'},
    {id: 9, name: 'Fluorine', date: 18.9984, text: 'F'},
    {id: 10, name: 'Neon', date: 20.1797, text: 'Ne'},
    {id: 11, name: 'Sodium', date: 22.9897, text: 'Na'},
    {id: 12, name: 'Magnesium', date: 24.305, text: 'Mg'},
    {id: 13, name: 'Aluminum', date: 26.9815, text: 'Al'},
    {id: 14, name: 'Silicon', date: 28.0855, text: 'Si'},
    {id: 15, name: 'Phosphorus', date: 30.9738, text: 'P'},
    {id: 16, name: 'Sulfur', date: 32.065, text: 'S'},
    {id: 17, name: 'Chlorine', date: 35.453, text: 'Cl'},
    {id: 18, name: 'Argon', date: 39.948, text: 'Ar'},
    {id: 19, name: 'Potassium', date: 39.0983, text: 'K'},
    {id: 20, name: 'Calcium', date: 40.078, text: 'Ca'},
  ];