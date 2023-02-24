import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form!: FormGroup;

  constructor(
    private router: Router,
    private authService: AuthService,
    private fb: FormBuilder,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.form = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
    })
  }

  onSignIn(){
    if(this.form.valid){
      const record = this.form.getRawValue();
      this.authService.register(record).subscribe({
        next: (data: any) =>{
          this.toastr.success("Registration is successfull");
  
          this.router.navigateByUrl('');
        },
        error: (e) => {
          this.toastr.error(e.error);
        }
      })
    }
    else{
      this.toastr.warning("Some fields are not finished.")
    }
  }

  backToHome(){
    this.router.navigateByUrl('')
  }

}
