import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  intrumentalists: any[] = [];

  //Search people paging
  readonly searchPeoplePageLimit = 4;
  needMorePage: boolean = false;
  sheets: any[] = [];
  allSheets: any[] = [];
  type: string = "Musicians";

  constructor(
    private authService: AuthService,
    private userService: UserService,
  ){

  }

  ngOnInit(): void {
    this.searchForBandmates();
  }

  searchForBandmates(){
    this.userService.searchForBandmates(this.authService.userID).subscribe({
      next: (data: any) => {
        console.log("Musicians: ",data);
        // this.intrumentalists = data;

        if(data.length > this.searchPeoplePageLimit){
          this.needMorePage = true;
        }
        this.allSheets = data;
      },
      error: (e) => {
        // this.toastr.error(e);
        console.log(e);
      }
    })
  }

  displaySheets(event: any){
    if(event){
      // console.log("Output sheet from child",event);
      this.intrumentalists = event;
    }
  }

}
