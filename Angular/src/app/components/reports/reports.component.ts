import { Component, OnInit } from '@angular/core';
import { ReportService } from '../../services/report.service';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  reports: any[] = [];
  reports1: any[] = [];
  displayedColumns: string[] = ['name', 'amount', 'date', 'category'];
  displayedColumns1: string[] = ['name', 'amount', 'date'];
  startDate?: Date;
  endDate?: Date;
  totalExpenses: number;
  totalIncomes: number;

  constructor(private reportService: ReportService) {
  }

  ngOnInit(): void {
    let todayDate = new Date(Date.now());
    this.startDate = new Date(todayDate.getDate());
    this.endDate = new Date(Date.now());
    this.loadReports();
  }

  loadReports(): void {
    this.reportService.getReports(this.startDate, this.endDate).subscribe(reports => {
      this.reports = reports;
      console.log(reports);
      this.loadSummary();
    });
    this.reportService.getReports1(this.startDate, this.endDate).subscribe(reports1 => {
      this.reports1 = reports1;
      console.log(reports1)
    });
  }

  exportPdf(): void {
    this.reportService.exportPdf(this.startDate, this.endDate).subscribe(data => {
      const blob = new Blob([data], {type: 'application/pdf'});
      saveAs(blob, 'report.pdf');
    });
  }

  exportCsv(): void {
    this.reportService.exportCsv(this.startDate, this.endDate).subscribe(data => {
      const blob = new Blob([data], {type: 'text/csv'});
      saveAs(blob, 'report.csv');
    });
  }

  loadSummary(): void {
    this.reportService.getSummary(this.startDate, this.endDate).subscribe(data => {
      this.totalExpenses = data.totalExpenses;
      this.totalIncomes = data.totalIncomes;
    });
  }
}
