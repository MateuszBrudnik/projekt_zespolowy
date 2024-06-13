import { Component, OnInit } from '@angular/core';
import { AnalysisService } from '../../services/analysis.service';
import { Analysis } from '../../models/analysis';

@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {

  public expenseTrends: any;
  public categoryExpenses: any;
  public incomeExpenseSummary: any;
  private userId = 'YOUR_USER_ID'; // Replace with actual user ID fetching logic

  constructor(private analysisService: AnalysisService) { }

  ngOnInit(): void {
    const startDate = new Date(Date.UTC(0, 0, 0, 0, 0, 0));
    const endDate = new Date();

    this.analysisService.getIncomeExpenseSummary(startDate, endDate, this.userId)
      .subscribe(data => this.incomeExpenseSummary = data, error => console.error(error));
    console.log(this.incomeExpenseSummary)

    this.analysisService.getSpendingTrends(startDate, endDate, this.userId)
      .subscribe(data => this.expenseTrends = data, error => console.error(error));
    console.log(this.expenseTrends)

    this.analysisService.getCategoryWiseExpenses(startDate, endDate, this.userId)
      .subscribe(data => this.categoryExpenses = data, error => console.error(error));
    console.log(this.categoryExpenses)
  }
}
