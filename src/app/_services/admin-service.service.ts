import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService extends BaseService {

  constructor(https: HttpClient) {
    super(https);
  }
  
  getAllUsers() {
    return this.http.get(this.API_URL + '/Admin/getAllUsers/');
  }
  
  getAllFeedback(){
    return this.http.get(this.API_URL  + '/Admin/getAllFeedBack');
  }
}
