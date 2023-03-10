import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
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

  // get photos(): FormArray { return this.profileForm.get('photos') as FormArray;}

  constructor(
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private fb: FormBuilder,
    private authService: AuthService,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    
    this.getIdFromRoute();

    console.log("this.recordId: ", this.recordId);
    if(this.recordId === null){
      this.getOwnProfile();
    }
    else{
      this.getProfile();
    }

    console.log(this.profileForm);
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
          photoUrl:['']
          // photos: this.fb.array([
          //   this.initPhotoForm()
          // ])
      })
    }
  };

  initPhotoForm(){
    return this.fb.group({
      url: [''],
      isMain: ['']
    })
  }

  getOwnProfile(){
    this.userService.getOwnProfile(this.authService.userID).subscribe({
      next: (data: any) => {
        console.log("User!: ", data);
        this.profileForm.patchValue(data);
      },
      error: (e) => {
        this.toastr.error(e);
      }
    })
  }

  getProfile(){
    this.userService.getOwnProfile(this.recordId).subscribe({
      next: (data: any) => {
        console.log("User!: ", data);
        this.profileForm.patchValue(data);
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
}
