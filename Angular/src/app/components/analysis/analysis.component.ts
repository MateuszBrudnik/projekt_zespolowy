import { Component, OnInit } from '@angular/core';
import { AnalysisService } from '../../services/analysis.service';
import { Analysis } from '../../models/analysis';

@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
  totalIncome: number;
  totalExpense: number;
  netBalance: number;
  expenseTrends: any[];
  trendLabels: string[];
  categoryExpenses: any[];
  categoryLabels: string[];
  chartOptions: any;

  constructor(private analysisService: AnalysisService) {
    this.chartOptions = {
      responsive: true,
      maintainAspectRatio: false
    };
  }

  ngOnInit(): void {
    this.loadAnalysis();
  }

  loadAnalysis(): void {
    const startDate = new Date(); // set start date
    const endDate = new Date(); // set end date
    this.analysisService.getReportSummary(startDate, endDate).subscribe(data => {
      this.totalIncome = data.totalIncomes;
      this.totalExpense = data.totalExpenses;
      this.netBalance = data.netBalance;
    });
    this.analysisService.getExpenseTrends(startDate, endDate).subscribe(data => {
      this.expenseTrends = data.map(item => item.total);
      this.trendLabels = data.map(item => item.date);
    });
    this.analysisService.getCategoryExpenses(startDate, endDate).subscribe(data => {
      this.categoryExpenses = data.map(item => item.total);
      this.categoryLabels = data.map(item => item.category);
    });
  }
}
