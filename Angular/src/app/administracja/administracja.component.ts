import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {MatIconModule} from '@angular/material/icon';
import {Timestamp} from "rxjs";
import {MAT_DIALOG_DATA, MatDialog, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
interface EXPENSES {
  name: String;
  expenseTime: Date;
  price: number;
}

interface EXPENSE {
  id: number;
  name: String;
  expenseTime: Date;
  price: number;
}

export interface DialogData {
  id: number
}
@Component({
  selector: 'app-administracja',
  templateUrl: './administracja.component.html',
  styleUrls: ['./administracja.component.css']
})
export class AdministracjaComponent implements OnInit {
  edytowano: string = "";
  Expenses: EXPENSE[] = [];
  constructor(private http: HttpClient, public dialog: MatDialog) {

  }
  ngOnInit() {
    this.http.get<any>('http://localhost:5170/Admin/Expenses',{responseType: 'json'} ).subscribe(result => {

      this.Expenses = result;
    }, error => console.log(error));
  }

  Usun(x: number){
    this.http.delete('http://localhost:5170/Admin/Delete?id=' + x,{responseType: 'text'} ).subscribe(result => {
      this.edytowano = result;
      sessionStorage.setItem("oplata", this.edytowano);
      alert(sessionStorage.getItem("oplata"));
      window.location.reload();
    }, error => console.log(error));
  }

  openDialog(uid: number): void {

    const dialogRef = this.dialog.open(DialogEdit, {
      data: {
        id: uid
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == 'success') {
        this.ngOnInit();
      }
    });
  }
}

@Component({
  selector: 'app-administracja-dialog',
  templateUrl: './administracja.dialog.component.html',
  styleUrls: ['./administracja.component.css']
})
export class DialogEdit implements OnInit{

  constructor(
    private http: HttpClient,
    public dialogRef: MatDialogRef<DialogEdit>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
    private _formBuilder: FormBuilder
  ) {

  }
  FormEdit: FormGroup;
  id: number;
  isPremium = this._formBuilder.group({
    isPremium: false,
  });

  ngOnInit() {
    this.FormEdit = new FormGroup({
      name: new FormControl("", [Validators.required]),
      expenseTime: new FormControl("", [Validators.required]),
      price: new FormControl("", [Validators.required])

    });
    this.getTicket(this.data.id);
  }

  getTicket = (id: number) => {
    const apiAddress: string = "http://localhost:5170/Admin/Expense?id=" + id;
    this.http.get<EXPENSE>(apiAddress, {responseType: 'json'})
      .subscribe({
        next: (com: EXPENSE) => {
          this.FormEdit.patchValue({
            id: com.id,
            name: com.name,
            expenseTime: com.expenseTime,
            price: com.price,
          });
          this.id = com.id;
        },
        error: (err: HttpErrorResponse) => console.log(err)
      })
  }

  SaveEdit = (FormValue: any) => {
    const dane = {...FormValue};

    const ForAuth: EXPENSE = {
      id: this.id,
      name: dane.name,
      expenseTime: dane.expenseTime,
      price: dane.price
    }
    const apiAddress: string = "http://localhost:5170/Admin/Update";
    this.http.put(apiAddress, ForAuth)
      .subscribe({
        next: () => {

          this.dialogRef.close("success");
        },
        error: (err: HttpErrorResponse) => {
          console.log(err)
        }
      })
  }

  onNoClick(): void {

    this.dialogRef.close();
  }


}
