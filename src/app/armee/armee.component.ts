import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-armee',
  templateUrl: './armee.component.html',
  styleUrls: ['./armee.component.css']
})
export class ArmeeComponent implements OnInit {
  //faction='light';
  faction='shadows';
  constructor() { }

  characters: Object[] = [
    
    { name: 'fantassin', life: '20', Dps:'10',heal:'0',qty:'10'},
    { name: 'vikings', life: '100', Dps:'50',heal:'0',qty:'10'},
    { name: 'arbalette', life: '150', Dps:'300',heal:'0' ,qty:'10'},
    { name: 'epeiste', life: '300', Dps:'300',heal:'0' ,qty:'10'},
    { name: 'soldat', life: '400', Dps:'400',heal:'0' ,qty:'10' },
    { name: 'archer', life: '400', Dps:'500',heal:'0',qty:'10' },
    { name: 'chaman', life: '400', Dps:'200',heal:'400' ,qty:'10'},
    { name: 'assassin', life: '500', Dps:'900',heal:'0' ,qty:'10'},
    { name: 'chevalier', life: '900', Dps:'500',heal:'0' ,qty:'10'},
    { name: 'surineur', life: '400', Dps:'1100',heal:'0' ,qty:'10' },
    { name: 'elementaliste', life: '750', Dps:'750',heal:'0' ,qty:'10'},
    { name: 'maraudeur', life: '1600', Dps:'200',heal:'0' ,qty:'10'},
    { name: 'mage', life: '500', Dps:'1300',heal:'0' ,qty:'10'},
    { name: 'gladiateur', life: '1900', Dps:'150',heal:'0' ,qty:'10'},
    { name: 'tank', life: '700', Dps:'100',heal:'0' ,qty:'10'},
    { name: 'elf', life: '2000', Dps:'0',heal:'1500' ,qty:'10'},
    { name: this.faction, life: '25000', Dps:'15000',heal:'0' ,qty:'1'},
    
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
