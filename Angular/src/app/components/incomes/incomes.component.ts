import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Income } from '../../models/income';
import { IncomeService } from '../../services/income.service';
import { AddIncomeDialogComponent } from './add-income-dialog/add-income-dialog.component';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {
  displayedColumns: string[] = ['name', 'amount', 'date', 'actions'];
  dataSource = new MatTableDataSource<Income>();

  constructor(private incomeService: IncomeService, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadIncomes();
  }

  loadIncomes(): void {
    this.incomeService.getIncomes().subscribe(incomes => {
      this.dataSource.data = incomes;
    });
  }

  openAddIncomeDialog(): void {
    const dialogRef = this.dialog.open(AddIncomeDialogComponent, {
      width: '250px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadIncomes();
      }
    });
  }

  deleteIncome(id: number): void {
    this.incomeService.deleteIncome(id).subscribe(() => {
      this.loadIncomes();
    });
  }
}
