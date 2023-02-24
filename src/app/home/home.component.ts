import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ModalContainerComponent } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ApiCallStatusCodes } from '../_enums/enums';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  loginForm!: FormGroup;

  get userName() { return this.loginForm.get("userName"); }
  get password() { return this.loginForm.get("password"); }

  // @ViewChild('loginModal', { static: true }) loginModal: ModalContainerComponent | undefined;

  constructor(
    private router: Router,
    private fb: FormBuilder,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
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
