import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SelectItem } from '../_helpers/selectItem';
import { AuthService } from '../_services/auth.service';
import { CodeListValuesService } from '../_services/code-list-values.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  instrumentalists: any[] = [];
  // selectedInstrument: any = "Instruments (Any)";
  bandmateSearchFormModel!: FormGroup;

  instruments : SelectItem[] = [];
  genres: SelectItem[] =[];
  selectedGenre: SelectItem = {
    label: 'Genre (Any)',
    value: null,
  };

  selectedInstrument: SelectItem = {
    label: 'Instrument (Any)',
    value: null,
  };

  //Search people paging
  readonly searchPeoplePageLimit = 4;
  needMorePage: boolean = false;
  sheets: any[] = [];
  allSheets: any[] = [];
  type: string = "Musicians";

  get name() { return this.bandmateSearchFormModel.get('name'); }

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthService,
    private userService: UserService,
    private codeListValueService: CodeListValuesService,
  ){

  }

  ngOnInit(): void {
    this.initializeFormModel();
    this.searchForBandmates();

    this.codeListValueService.getGenres().subscribe({
      next: (data: any) => {
        this.genres = data.map((d:any) => {
          return { label: d.name, value: d.id }
        });
        console.log("Genres: ", data);
      },
      error: (e) => {
        console.log(e);
      }
    })

    this.codeListValueService.getInstruments().subscribe({
      next: (data: any) => {
        this.instruments = data.map((d:any) => {
          return { label: d.name, value: d.id }
        });
        console.log("Instruments: ", data);
      },
      error: (e) => {
        console.log(e);
      }
    })
  }

  initializeFormModel(){
    if(!this.bandmateSearchFormModel){
      this.bandmateSearchFormModel = this.fb.group({
        name: [''],
      })
    }
  }

  searchForBandmates(){
    let params = {
      userId: this.authService.userID,
      genreId: this.selectedGenre.value,
      instrumentId: this.selectedInstrument.value,
      name: this.name?.value,
    }
    console.log("search for bandmates: ", params);

    this.userService.searchForBandmates(params).subscribe({
      next: (data: any) => {
        console.log("Musicians: ",data);
        // this.instrumentalists = data;

        if(data.length > 0){
          if(data.length > this.searchPeoplePageLimit){
            this.needMorePage = true;
          }
        }
        else{
          this.instrumentalists = [];
        }

        this.allSheets = [];
        this.allSheets = data;
        
        console.log("allsheets: ", this.allSheets);
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
      if(this.instrumentalists.length > 0){
        this.instrumentalists = [];
        console.log("delete sheets: ", this.instrumentalists, event);
      }
      this.instrumentalists = event;
    }
  }

  onSelectInstrument(ins: any){
    console.log("Selected inst: ", ins);
    this.selectedInstrument = ins;
    this.searchForBandmates();
  }

  onSelectGenre(genre: any){
    this.selectedGenre = genre;
    this.searchForBandmates();
  }

  visitProfile(id: any){
    this.router.navigateByUrl('edit-profile/' + id);
  }

}
