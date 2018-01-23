import { Injectable } from '@angular/core';
import { Ressource } from '../_models/Ressource';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class RessourceService {

  constructor(private http: HttpClient) { }
  
      getAllRessourcebyUser(test: Text) {
          console.log('test');
           return this.http.post<Ressource[]>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_GetAll_RessourceJoueur',{sUUID:test}).map(ressource => {
                   localStorage.setItem('ressourceUser', JSON.stringify(ressource));
                   console.log(localStorage);
           });;
      }
  
  
      usePotionbyId(uid: Text,id: number) {
          console.log(uid);
          return this.http.post('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx?op=BC_Consommer_PotionJoueur',{sUUID: '307c7442-5da2-4c4e-8199-2d12fe21533d', iIdPotion: id, iQtePotion: 1});
      }

}
