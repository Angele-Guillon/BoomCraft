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

