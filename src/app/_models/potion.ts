export class Potion {
    id: number;
    power: number;
    qty: number;
    type: string;
    name:string;
   

    public constructor(id: number, power:number,qty: number,type:string){
        this.id = id;
        this.power = power;
        this.qty = qty;
        this.type = type;
    }
    
}