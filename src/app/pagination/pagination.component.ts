import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.css']
})
export class PaginationComponent implements OnInit, OnChanges {

  all: any[] = [];
  sheets: any[] = [];
  needMorePage: boolean = false;

  // @Input() sheets: any;
  // @Input() needMorePage: any;
  @Input() allSheets: any;
  @Input() pageLimit: any;
  @Input() type: any;

  @Output() outputSheets = new EventEmitter();

  
  ngOnChanges(changes: SimpleChanges): void {
    console.log("Pagination changes (Child): ", changes);

    for (const propName in changes) {
      if(propName === 'allSheets' && changes['allSheets'].currentValue.length > 0){
        this.showFirstPage();
      }
      if(propName === 'allSheets' && changes['allSheets'].currentValue.length === 0){
        this.needMorePage = false;
      }
    }
  }
  ngOnInit(): void {
    // console.log("all sheets on init: ", this.allSheets);
    // this.showFirstPage();
  }

  sheetsLeft: any = 0;
  lastSheetNo: any = 0;
  lastLastSheetNo: any = 0;
  hasPrevPage: boolean = false;

  showFirstPage(){
    console.log("show first page", this.allSheets,this.pageLimit, this.type)
    if(this.allSheets !== undefined && this.allSheets.length > 0 
        && this.pageLimit !== undefined && this.pageLimit > 0){

      // console.log("All sheets show first page: ", this.allSheets, this.pageLimit);
      let lastIndex: any;
      if(this.allSheets.length >= this.pageLimit){
        lastIndex = this.pageLimit
      }
      else{
        lastIndex = this.allSheets.length;
      }
      // this.outputSheets.emit(this.sheets);
      this.sheets = [];
      for(let index = 0; index < lastIndex; index++){
        this.sheets.push(this.allSheets[index]);
      }
      this.lastSheetNo = this.pageLimit;
      this.sheetsLeft = this.allSheets.length - this.lastSheetNo;
      this.showStatus();
      this.outputSheets.emit(this.sheets);
      console.log("All sheets show first page: ", this.allSheets, this.pageLimit);
      this.allSheets.length <= this.pageLimit ? this.needMorePage = false : this.needMorePage = true;
      // console.log("Sheets: ", this.sheets);
    }
  }

  showNextPage(){
    while(this.sheets.length > 0){
      this.sheets.pop();
    }

    this.hasPrevPage = true;

      if(this.sheetsLeft >= this.pageLimit){

        for(let index = this.lastSheetNo; index < (this.lastSheetNo + this.pageLimit); index++){
          this.sheets.push(this.allSheets[index]);
        }
    
        this.lastLastSheetNo = this.lastSheetNo;
        this.lastSheetNo = this.lastSheetNo + this.pageLimit;
        this.sheetsLeft = this.allSheets.length - this.lastSheetNo;

        this.showStatus();

        if(this.sheetsLeft === 0){
          this.needMorePage = false;
        }

      }
      else if(this.sheetsLeft < this.pageLimit){
        for(let index = this.lastSheetNo; index < this.allSheets.length; index++){
          this.sheets.push(this.allSheets[index]);
        }
        
        this.needMorePage = false;
        this.lastLastSheetNo = this.lastSheetNo;
        this.lastSheetNo = this.lastSheetNo + this.sheetsLeft;
        this.sheetsLeft = this.allSheets.length - this.lastSheetNo;
        this.showStatus();
      }
  }

  showPrevPage(){
    while(this.sheets.length > 0){
      this.sheets.pop();
    }
    let firstSheet = this.lastSheetNo - (this.pageLimit-1);

    if(firstSheet > 0 && this.sheetsLeft != 0){
      for(let index = firstSheet - (this.pageLimit + 1); index < (firstSheet - 1); index++){
        this.sheets.push(this.allSheets[index]);
      }
      
      this.lastLastSheetNo = this.lastSheetNo;
      this.lastSheetNo = firstSheet - 1;
      this.sheetsLeft = this.allSheets.length - this.lastSheetNo;
      
      // this.showStatus();

      if(this.lastSheetNo === this.pageLimit){
        this.hasPrevPage = false;
      }
    }
    else if(this.sheetsLeft === 0){
      for(let index = this.lastLastSheetNo - this.pageLimit; index < this.lastLastSheetNo; index++){
        this.sheets.push(this.allSheets[index]);
      }

      this.sheetsLeft = this.lastSheetNo - this.lastLastSheetNo;
      this.lastSheetNo = this.lastLastSheetNo;
      this.needMorePage = true;

      // this.showStatus();
      if(this.lastSheetNo === this.pageLimit){
        this.hasPrevPage = false;
      }
    }
  }

  showStatus(){
    // console.log("Sheets:", this.sheets, this.allSheets);
  }
}
