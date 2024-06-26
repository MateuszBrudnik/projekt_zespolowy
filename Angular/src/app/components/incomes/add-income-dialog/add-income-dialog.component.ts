import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { IncomeService } from '../../../services/income.service';
import { Income } from '../../../models/income';

@Component({
  selector: 'app-add-income-dialog',
  templateUrl: './add-income-dialog.component.html',
  styleUrls: ['./add-income-dialog.component.css']
})
export class AddIncomeDialogComponent implements OnInit {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private incomeService: IncomeService,
    public dialogRef: MatDialogRef<AddIncomeDialogComponent>
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      amount: ['', Validators.required],
      date: ['', Validators.required]
    });
  }

  ngOnInit(): void {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.form.valid) {
      const newIncome: Income = {
        id: 0,
        ...this.form.value,
        userId: 'id' // userId will be set on the backend
      };
      this.incomeService.addIncome(newIncome).subscribe({
        next: () => {
          this.dialogRef.close(true);
        },
        error: (error) => {
          if (error.status === 400 && error.error && error.error.Errors) {
            console.log('Validation Errors:', error.error.Errors);
            alert('Validation Errors: ' + error.error.Errors.join(', '));
          } else {
            console.error('An error occurred:', error);
          }
        }
      });
    }
  }


}
