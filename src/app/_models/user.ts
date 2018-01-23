export class User {
    id: number;
    globalId: Text;
    name: string;
    password: string;
    email: string;
    faction: string;

    public constructor(id: number, globalId: Text, name: string, password: string, email: string, faction: string){
        this.id = id;
        this.globalId = globalId;
        this.name = name;
        this.password = password;
        this.email = email;
        this.faction = faction;
    }
    
}

