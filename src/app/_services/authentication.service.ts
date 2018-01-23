import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }

    login(username: string, password: string) {
        return this.http.post<any>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_ObtenirJoueur', { sNomUtilisateur: username, sMdp: password })
            .map(user => {
               if (user.user){
                    localStorage.setItem('currentUser', JSON.stringify(user.user));
                    console.log(localStorage);
               }
                return user;
            });
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        localStorage.removeItem('potion');
        localStorage.removeItem('ressourceUser');
    }
}