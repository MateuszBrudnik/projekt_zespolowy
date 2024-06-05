import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-add-category-dialog',
  templateUrl: './add-category-dialog.component.html',
  styleUrls: ['./add-category-dialog.component.css']
})
export class AddCategoryDialogComponent {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private dialogRef: MatDialogRef<AddCategoryDialogComponent>
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]]
    });
  }

  onSave(): void {
    if (this.form.valid) {
      this.categoryService.addCategory(this.form.value).subscribe({
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
}
