import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'client';

  loggedIn: any = false;

  loggedOut(event:any){
    if(event){
      console.log("LOGGED out (Parents - App Component!): ", event);
    }
  }

  userHasLoggedIn(event:any){
    console.log("User has logged in! (Parents - App Component): ", event);

    if(event){
      console.log("User has logged in! (Parents - App Component): ", event);
    }
  }
}
