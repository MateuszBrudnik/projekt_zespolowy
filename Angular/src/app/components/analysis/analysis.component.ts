import { Component, OnInit } from '@angular/core';
import { AnalysisService } from '../../services/analysis.service';
import { Analysis } from '../../models/analysis';

@Component({
  selector: 'app-analysis',
  templateUrl: './analysis.component.html',
  styleUrls: ['./analysis.component.css']
})
export class AnalysisComponent implements OnInit {
  analysis: Analysis;

  constructor(private analysisService: AnalysisService) { }

  ngOnInit(): void {
    this.loadAnalysis();
  }

  loadAnalysis(): void {
    this.analysisService.getAnalysis().subscribe(analysis => {
      this.analysis = analysis;
    });
  }
}
