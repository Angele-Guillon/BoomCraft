import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { User, Potion} from '../_models/index';


@Injectable()
export class PotionService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Potion[]>('/api/users');
    }

    usePotionbyId(id: number) {
        return this.http.get('/api/users/' + id);
    }

    
}