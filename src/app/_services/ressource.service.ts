import { Injectable } from '@angular/core';
import { Ressource } from '../_models/Ressource';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RessourceService {

  constructor(private http: HttpClient) { }
  
      getAllRessourcebyUser(id: number) {
         console.log(id);
           return this.http.post<Ressource[]>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_GetAll_RessourceJoueur',{iIdJoueur: id}).map(ressource => {
                   localStorage.setItem('ressourceUser', ressource.toString());
           });;
      }
  


}
