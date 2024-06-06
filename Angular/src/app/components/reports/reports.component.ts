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
  displayedColumns: string[] = ['name', 'amount', 'date', 'category'];
  startDate?: Date;
  endDate?: Date;

  constructor(private reportService: ReportService) { }

  ngOnInit(): void {
    this.loadReports();
  }

  loadReports(): void {
    this.reportService.getReports(this.startDate, this.endDate).subscribe(reports => {
      this.reports = reports;
    });
  }

  exportPdf(): void {
    this.reportService.exportPdf(this.startDate, this.endDate).subscribe(data => {
      const blob = new Blob([data], { type: 'application/pdf' });
      saveAs(blob, 'report.pdf');
    });
  }

  exportCsv(): void {
    this.reportService.exportCsv(this.startDate, this.endDate).subscribe(data => {
      const blob = new Blob([data], { type: 'text/csv' });
      saveAs(blob, 'report.csv');
    });
  }
}
