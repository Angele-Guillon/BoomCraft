import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, Potion} from '../_models/index';

@Injectable()
export class PotionService {
    constructor(private http: HttpClient) { }

    getAllPotion(test: Text) {
        console.log(test);
         return this.http.post<Potion[]>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_GetAll_PotionJoueur',{sUUID: '307c7442-5da2-4c4e-8199-2d12fe21533d'}).map(potion => {
                 localStorage.setItem('potion', JSON.stringify(potion));
                 console.log(localStorage);
         });;
    }


    usePotionbyId(uid: Text,id: number) {
        console.log(uid);
        return this.http.post('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx?op=BC_Consommer_PotionJoueur',{sUUID: '307c7442-5da2-4c4e-8199-2d12fe21533d', iIdPotion: id, iQtePotion: 1});
    }

    
}