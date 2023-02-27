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
        console.log(data);
        this.intrumentalists = data;
      },
      error: (e) => {
        // this.toastr.error(e);
        console.log(e);
      }
    })
  }

}
