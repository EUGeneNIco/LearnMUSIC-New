import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  profileForm!:FormGroup
  editMode: boolean = false;
  recordId: any = 0;

  get aboutMe() { return this.profileForm.get('aboutMe'); }
  get bio() { return this.profileForm.get('bio'); }
  get email() { return this.profileForm.get('aboutMe'); }
  get firstName() { return this.profileForm.get('firstName'); }
  get lastName() { return this.profileForm.get('lastName'); }
  get codeName() { return this.profileForm.get('codeName'); }
  get passwordHash() { return this.profileForm.get('passwordHash'); }
  get photoUrl() { return this.profileForm.get('photoUrl'); }

  get instruments(): FormArray { return this.profileForm.get('instruments') as FormArray;}

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private authService: AuthService,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    
    this.getIdFromRoute();
    // console.log("this.recordId: ", this.recordId);
    if(this.recordId === null){
      this.displayOwnProfile();
    }
    else{
      this.getProfile();
    }
  }

  initializeForm() {
      if(!this.profileForm){
        this.profileForm = this.fb.group({
          aboutMe: [''],
          codeName: [''],
          bio: [''],
          email: [''],
          firstName: [''],
          lastName: [''],
          userName: [''],
          passwordHash: [''],
          photoUrl:[''],
          instruments: this.fb.array([
            this.initInstrumentForm()
          ])
      })
    }
  };

  initInstrumentForm(){
    return this.fb.group({
      instrument: [''],
    })
  }

  displayOwnProfile(){
    const userData = this.authService.userObjectFromStorage;
    const userInstruments = userData.instruments;
    if(userInstruments.length > 1){
      for (let index = 1; index < userInstruments.length; index++) {
        this.instruments.push(this.initInstrumentForm());
      }
    }
    this.profileForm.patchValue(userData);
    console.log("Profile Form: ", this.profileForm);
  }

  getProfile(){
    this.userService.getOwnProfile(this.recordId).subscribe({
      next: (data: any) => {
        console.log("User!: ", data);
        this.profileForm.patchValue(data);
        // console.log("Profile Form: ", this.profileForm, this.instruments);
      },
      error: (e) => {
        this.toastr.error(e);
      }
    })
  }

  getIdFromRoute(){
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        this.recordId = id;
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  goToDashboard(){
    this.router.navigateByUrl('dashboard');
  }
}
