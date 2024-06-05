import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ExpenseService } from '../../services/expense.service';
import { Expense } from '../../models/expense';
import { AddExpenseDialogComponent } from './add-expense-dialog/add-expense-dialog.component';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
  displayedColumns: string[] = ['name', 'amount', 'date', 'category', 'actions'];
  dataSource = new MatTableDataSource<Expense>();

  constructor(
    private expenseService: ExpenseService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.loadExpenses();
  }

  loadExpenses(): void {
    this.expenseService.getExpenses().subscribe(expenses => {
      this.dataSource.data = expenses;
    });
  }

  deleteExpense(id: number): void {
    this.expenseService.deleteExpense(id).subscribe(() => {
      this.loadExpenses();
      this.snackBar.open('Expense deleted', 'Close', { duration: 2000 });
    });
  }

  openAddExpenseDialog(): void {
    const dialogRef = this.dialog.open(AddExpenseDialogComponent, {
      width: '400px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadExpenses();
        this.snackBar.open('Expense added', 'Close', { duration: 2000 });
      }
    });
  }
}
