import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NotificationMessages } from '../_enums/notification-messages';
import { AuthService } from '../_services/auth.service';
import { SongSheetService } from '../_services/song-sheet.service';

@Component({
  selector: 'app-song-sheets',
  templateUrl: './song-sheets.component.html',
  styleUrls: ['./song-sheets.component.css']
})
export class SongSheetsComponent implements OnInit {

  title = 'LearnMUSIC';
  
  needMorePage: boolean = false;
  hasPrevPage: boolean = false;
  songSheets: any[] = [];
  allSheets: any[] = [];
  videos: any[] = [];

  //Paging
  readonly pageLimit: any = 6;
  type: string = "Songsheets"


  // @Input() getSongsheets:any = false;


  constructor(
    private auth: AuthService,
    private toastr: ToastrService,
    private songSheetService: SongSheetService, 
    private fb: FormBuilder,
    public router: Router,
    private route: ActivatedRoute,)
    { }
  
  ngOnInit(): void {
    this.reloadData();
  }

  reloadData(){
    this.needMorePage = false;
    this.hasPrevPage = false;
    this.songSheets = [];
    this.allSheets = [];
    this.getAllCards();
  }

  getAllCards(){
    this.songSheetService.getAllSheets(this.auth.userID).subscribe({
      next: (data: any) => {
        // console.log("Songs: " ,data);
        
        if(data.length > this.pageLimit){
          this.needMorePage = true;
        }
        
        this.allSheets = data;
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  
  displaySheets(event: any){
    if(event){
      // console.log("Output sheet from child songsheet",event);
      this.songSheets = event;
    }
  }

  viewSongSheetDetails(sheetId: any){
    // this.router.navigateByUrl('chords/' + sheetId);
  }

  addSongSheet(){
    // this.router.navigateByUrl('/chords/detail');
  }

  deleteSongSheet(sheetId: any){
    console.log(sheetId);
    this.songSheetService.delete(sheetId).subscribe({
      next: (data: any) => {
        // console.log(data);
        this.toastr.success(NotificationMessages.DeleteSuccessful.Message)
        setTimeout(() => this.reloadData(), 500);
      },
      error: (e) => {
        this.toastr.error(e.error);
      }
    })
  }

  editSongSheet(sheetId: any){
    // this.router.navigateByUrl('chords/detail/' + sheetId);
  }
}
