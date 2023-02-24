import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SongSheetService {

  baseApiUrl: string = environment.baseApiURL
  constructor(private http: HttpClient) { }
  
  getAllSheets(userId: any) {
    return this.http.get(this.baseApiUrl + '/SongSheets/getAll/'+ userId);
  }

  getSheetById(id: any) {
    return this.http.get(this.baseApiUrl + '/SongSheets/' + id);
  }

  updateSheet(record:any){
    return this.http.post(this.baseApiUrl + '/SongSheets/update', record);
  }

  addSongSheet(record:any){
    return this.http.post(this.baseApiUrl + '/SongSheets', record);
  }

  delete(id: any){
    return this.http.post(this.baseApiUrl + '/SongSheets/delete/' + id, null);
  }



}
