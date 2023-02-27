import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(https: HttpClient) {
    super(https);
  }

  getOwnProfile(id: any) {
    return this.http.get(this.API_URL + '/User/getUserProfile/' + id);
  }

  searchForBandmates(id: any) {
    return this.http.get(this.API_URL + '/User/searchBandmates/' + id);
  }
}
