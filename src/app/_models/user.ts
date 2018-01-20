export class User {
    id: number;
    id_global: Text;
    name: string;
    password: string;
    email: string;
    faction: string;

    public constructor(id: number, id_global: Text, name: string, password: string, email: string, faction: string){
        this.id = id;
        this.id_global = id_global;
        this.name = name;
        this.password = password;
        this.email = email;
        this.faction = faction;
    }
    
}

//user creation Api
export class APICUser {
    
      public name: string;
      public password: string;
      public email: string;
      public faction: string;
    
      public constructor(name: string, password: string, email: string, faction: string){
        this.name = name;
        this.password = password;
        this.email = email;
        this.faction = faction;
      }
    }