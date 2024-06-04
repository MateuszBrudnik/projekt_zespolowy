import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ReportService } from '../../services/report.service';
import { Expense } from '../../models/expense';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  displayedColumns: string[] = ['name', 'amount', 'date', 'category'];
  dataSource = new MatTableDataSource<Expense>();

  startDate: Date;
  endDate: Date;
  category: string;

  constructor(private reportService: ReportService) { }

  ngOnInit(): void {
  }

  loadReport(): void {
    this.reportService.getFilteredReport(this.startDate, this.endDate, this.category).subscribe(expenses => {
      this.dataSource.data = expenses;
    });
  }

  downloadPdf(): void {
    this.reportService.getPdfReport().subscribe(blob => {
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'report.pdf';
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    });
  }
}
