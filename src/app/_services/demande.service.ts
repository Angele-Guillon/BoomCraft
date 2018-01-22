import { Jsonp } from "@angular/http";
import { Injectable } from "@angular/core";

@Injectable()
export class DemandeService {
  apiRoot: string = 'http://boomcraft.masi-henallux.be:8080/apiLocal.asmx/demande';

  constructor(private jsonp: Jsonp) {
  }

  getdemande(model: any) {
    let apiURL = `${this.apiRoot}?term=${model.faction}`;
    return this.jsonp.request(apiURL)
        .map(res => {
          return res.json().results.map(item => {
            return new Demande(
                item.id,
                item.nbUnit
            );
          });
        });
  }
}