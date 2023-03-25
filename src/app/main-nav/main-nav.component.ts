import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';
import { User } from "../_models/User";

@Component({
  selector: 'app-main-nav',
  templateUrl: './main-nav.component.html',
  styleUrls: ['./main-nav.component.css']
})
export class MainNavComponent implements OnChanges {
  
  isAdmin: boolean = false;
  currentUser: User = {
    username: '',
    codeName: '',
    bio: '',
    aboutMe: '',
    firstName: '',
    lastName: '',
    email: '',
    photoUrl: '',
  }

  @Input() loggedIn:any;
  @Output() loggedOut = new EventEmitter();

  constructor(
    private toastr: ToastrService,
    public authService: AuthService,
    private router: Router,
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    // console.log("Changes from main nav: ",changes['loggedIn'].currentValue)
    for (const propName in changes) {
      if(propName === 'loggedIn' && changes['loggedIn'].currentValue === true){
        this.loggedIn = true; 
      }
    }
  }

  ngOnInit(): void {
    this.checkIfSignedIn();
    // console.log("A User is Logged In ? (main nav): ", this.authService.userObjectFromStorage);
    this.currentUser = this.authService.userObjectFromStorage;

    this.authService.currentUser$ === null ? this.loggedIn = false : this.loggedIn = true;
  }

  signOut() {
    this.authService.destroyAuthToken();
    this.authService.setCurrentUserToNull();
    this.authService.destroyUserObjectFromStorage();
    // console.log("Current user: ", this.authService.currentUser$, this.authService.userFromStorage);

    this.loggedIn = false;

    this.loggedOut.emit(true);

    this.router.navigateByUrl('');
    
    this.toastr.success("Logged out.");
  }

  goToLogInPage(){
    this.router.navigateByUrl('login');
  }

  checkIfSignedIn(){
    this.loggedIn = this.authService.authToken !== null 
      ? this.authService.setCurrentUser() 
      : this.authService.setCurrentUserToNull();
  }

  checkIfAdmin(){
    this.authService.userHasAccessToModule("Admin")  ? this.isAdmin = true : this.isAdmin = false;
  }

  goToDashboard(){
    this.router.navigateByUrl('dashboard');
  }

  viewProfile(){
    this.router.navigateByUrl('edit-profile');
  }
}
