import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ApiCallStatusCodes } from '../_enums/enums';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form!: FormGroup;
  @Output() userHasLoggedIn = new EventEmitter();

  constructor(
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder,
    public toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.form = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

  backToHome(){
    this.router.navigateByUrl('');
  }

  onLogIn(){
    if(this.form.valid){
      const record = this.form.getRawValue();
      // console.log(record);
      
      this.authService.login({
        username: record.userName,
        password: record.password,
      }).subscribe({
        next:(data: any) => {
          console.log("token ", JSON.parse(data) );
					this.authService.authToken = JSON.parse(data).token;
          this.authService.userToStorage = JSON.stringify(JSON.parse(data));
          this.authService.setCurrentUser();
          console.log("Current user: ", this.authService.currentUser$, this.authService.userFromStorage);
          // console.log("Orig Token: ", data);
          // console.log("Parsed Token: ", this.authService.authToken);
          // console.log("Parsed Token (Subject): ", this.authService.authToken.split('.')[1]);
          // console.log("JSON (after atob) ",this.authService.atob);
          // console.log("Object (Payload): ",this.authService.payLoad);
          // console.log("User id: ",this.authService.userID);
          // console.log("Name: ",this.authService.userName);
          // console.log("Modules: ",this.authService.accessibleModulesOfUser);

          this.toastr.success("Welcome Back.");

          this.userHasLoggedIn.emit(true);
          
          setTimeout(() => this.router.navigateByUrl('dashboard') , 500);
        },
        error: (e) => {

					if (e.status == 0) {
            this.toastr.error("Server is not available. Please try again later");
					}
					else if (e.status == ApiCallStatusCodes.UNAUTHORIZED) {
            this.toastr.error(e.error)
					}
					else {
            this.toastr.error(e.error)
					}
        }
      })
    }
    else{
      this.toastr.warning("Enter username and password.")
    }
  }

}
