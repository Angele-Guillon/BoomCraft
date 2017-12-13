import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-subscribe',
  templateUrl: './subscribe.component.html',
  styleUrls: ['./subscribe.component.css']
})
export class SubscribeComponent implements OnInit {

  dataressources = new MatTableDataSource<Factions>(RESSOURCE_DATA);
  constructor() { }

  ngOnInit() {

  }

}
export interface Factions {
  name: string ;
  id: number ;
}

 const RESSOURCE_DATA: Factions[] = [
    {id: 1, name: 'Shadow'},
    {id: 2, name: 'light'},
  ];