import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User, Potion} from '../_models/index';

@Injectable()
export class PotionService {
    constructor(private http: HttpClient) { }

    getAllPotion(test: Text) {
        console.log(test);
         return this.http.post<Potion[]>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_GetAll_PotionJoueur',{sUUID: test}).subscribe(potion => {
                 localStorage.setItem('potion', JSON.stringify(potion));
                // console.log(localStorage);
         });;
    }


    usePotionbyId(uid: Text,id: number) {
        console.log(uid);
        return this.http.post('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_Consommer_PotionJoueur',{sUUID: uid, iIdPotion: id, iQtePotion: 1}).subscribe();
    }

    
}