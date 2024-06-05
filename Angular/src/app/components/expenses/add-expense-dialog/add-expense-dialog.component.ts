import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ExpenseService } from '../../../services/expense.service';
import { CategoryService } from '../../../services/category.service';
import { Category } from '../../../models/category';
import { AddCategoryDialogComponent } from '../../categories/add-category-dialog/add-category-dialog.component';

@Component({
  selector: 'app-add-expense-dialog',
  templateUrl: './add-expense-dialog.component.html',
  styleUrls: ['./add-expense-dialog.component.css']
})
export class AddExpenseDialogComponent implements OnInit {
  form: FormGroup;
  categories: Category[] = [];

  constructor(
    private fb: FormBuilder,
    private expenseService: ExpenseService,
    private categoryService: CategoryService,
    private dialogRef: MatDialogRef<AddExpenseDialogComponent>,
    private dialog: MatDialog
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      amount: ['', [Validators.required, Validators.min(0.01)]],
      date: ['', Validators.required],
      categoryId: ['', Validators.required],
      userId: "id"
    });
  }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  onSave(): void {
    if (this.form.valid) {
      this.expenseService.addExpense(this.form.value).subscribe({
        next: () => this.dialogRef.close(true),
        error: (error) => {
          if (error.status === 400 && error.error && error.error.errors) {
            console.error('Validation Errors:', error.error.errors);
            alert('Validation Errors: ' + JSON.stringify(error.error.errors));
          } else {
            console.error('An error occurred:', error);
          }
        }
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  openAddCategoryDialog(): void {
    const dialogRef = this.dialog.open(AddCategoryDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.categoryService.getCategories().subscribe(categories => {
          this.categories = categories;
        });
      }
    });
  }
}
