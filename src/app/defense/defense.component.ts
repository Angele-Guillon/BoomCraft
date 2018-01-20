import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-defense',
  templateUrl: './defense.component.html',
  styleUrls: ['./defense.component.css']
})
export class DefenseComponent implements OnInit {

  icon:any[];
  ressource:number=400;

  buildings: any[] = [
    
    { id:1 , name: 'barrack', wood: 20, stone:10, gold:0, lvl:0},
    { id:2 , name: 'castel', wood: 100, stone:50, gold:0, lvl:10},
    { id:3 , name: 'forest', wood: 150, stone:300, gold:0 , lvl:10},
    { id:4 , name: 'hideaway', wood: 300, stone:300, gold:0 , lvl:10},
    { id:5 , name: 'magic-tower', wood: 400, stone:400, gold:0 , lvl:10 },
    { id:6 , name: 'shooting-stand',wood: 400, stone:500, gold:0, lvl:10 },
    { id:7 , name: 'stable', wood: 400, stone:200, gold:400 , lvl:10},
    { id:8 , name: 'tavern', wood: 500, stone:900, gold:0 , lvl:10},
    { id:9 , name: 'temple', wood: 900, stone:500, gold:0 , lvl:10},
    
  ];
  constructor() { }

  ngOnInit() {
  }

  levelplus(){
 
  }

}
