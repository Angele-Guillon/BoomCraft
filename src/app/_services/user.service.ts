import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User } from '../_models/index';

@Injectable()
export class UserService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<User[]>('/api/users');
    }

    getById(id: number) {
        return this.http.get('/api/users/' + id);
    }

    create(model: any) {
        let url = 'http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_CreerJoueur';
        console.log(model.faction);
        return this.http.post('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_CreerJoueur', {sNomUtilisateur: model.username, sMdp:model.password,sEmail:model.mail,iFaction:model.faction});
    }

    update(user: User) {
        return this.http.put('/api/users/' + user.id, user);
    }

    delete(id: number) {
        return this.http.delete('/api/users/' + id);
    }
}