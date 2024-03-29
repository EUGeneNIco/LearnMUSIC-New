import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalContainerComponent } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ApiCallStatusCodes } from '../_enums/enums';
import { AuthService } from '../_services/auth.service';
import { ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  loggedIn: boolean = false;
  loginForm!: FormGroup;

  get userName() { return this.loginForm.get("userName"); }
  get password() { return this.loginForm.get("password"); }

  // @ViewChild('loginModal', { static: true }) loginModal: ModalContainerComponent | undefined;

  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initializeForm();

    this.loggedIn = this.authService.authToken !== null ? true : false;

  }

  initializeForm(){
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  signUp(){
    this.router.navigateByUrl('register');
  }


}
