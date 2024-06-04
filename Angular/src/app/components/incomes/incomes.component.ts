import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IncomeService } from '../../services/income.service';
import { Income } from '../../models/income';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {
  displayedColumns: string[] = ['source', 'amount', 'date', 'actions'];
  dataSource = new MatTableDataSource<Income>();

  constructor(private incomeService: IncomeService, private snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.loadIncomes();
  }

  loadIncomes(): void {
    this.incomeService.getIncomes().subscribe(incomes => {
      this.dataSource.data = incomes;
    });
  }

  deleteIncome(id: number): void {
    this.incomeService.deleteIncome(id).subscribe(() => {
      this.loadIncomes();
      this.snackBar.open('Income deleted', 'Close', { duration: 2000 });
    });
  }
}
