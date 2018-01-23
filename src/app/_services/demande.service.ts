import { Jsonp } from "@angular/http";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class DemandeService {
  apiRoot: string = 'http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/demande';

  constructor(private jsonp: Jsonp,private http: HttpClient) {
  }

  getALLDemandeTroupe() {
    console.log(localStorage);
    return this.http.post<Demande[]>('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_GetAll_DemandeTroupe','').subscribe(demande => {
      localStorage.setItem('demandeTroupe', demande.toString());
      console.log(localStorage);
});;
  }

  postdemande(model: any,uid: Text,ifac: number) {
    console.log(model);
    return this.http.post('http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/BC_DemanderRessource',{sUUID:uid,iFaction:ifac,iQteWood: model.wood,iQteFood: model.food,iQteGold: model.gold,iQteRock: model.stone}).subscribe();
    }
}