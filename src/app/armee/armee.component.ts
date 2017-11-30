import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-armee',
  templateUrl: './armee.component.html',
  styleUrls: ['./armee.component.css']
})
export class ArmeeComponent implements OnInit {
  faction='light';
  //faction='shadows';
  constructor() { }

  characters: Object[] = [
    { name: 'fantassin', life: '50', Dps:'10',heal:'0',qty:'10'},
    { name: 'elementaliste', life: '100', Dps:'10',heal:'0',qty:'10'},
    { name: 'archer', life: '150', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'arbalette', life: '200', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'soldat', life: '200', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'surineur', life: '200', Dps:'10',heal:'0' ,qty:'10' },
    { name: 'assasin', life: '250', Dps:'10',heal:'0',qty:'10' },
    { name: 'chaman', life: '300', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'elf', life: '350', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'chevalier', life: '400', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'epeiste', life: '450', Dps:'10',heal:'0' ,qty:'10' },
    { name: 'gladiateur', life: '550', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'guerrier-tribal', life: '650', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'mage', life: '750', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'maraudeur', life: '850', Dps:'10',heal:'0' ,qty:'10'},
    { name: this.faction, life: '950', Dps:'10',heal:'0' ,qty:'10'},
    { name: 'tank', life: '1150', Dps:'10',heal:'0' ,qty:'10'},
   
  ];

  public sendattack(name:string,nb:number){
    return name+": "+nb;
  }
  public senddefense(name:string,nb:number){
   
    return name+": "+nb;
    
  }
  public deleteunit(name:string,nb:number){
    return name+": "+nb;
  }

  ngOnInit() {
    
  }
 
}
