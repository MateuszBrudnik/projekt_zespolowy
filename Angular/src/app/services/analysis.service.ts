import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Analysis} from "../models/analysis";

@Injectable({
  providedIn: 'root'
})
export class AnalysisService {
  private apiUrl = 'http://localhost:5170/api/analysis';

  constructor(private http: HttpClient) { }
  getAnalysis(): Observable<Analysis> {
    return this.http.get<Analysis>(this.apiUrl);
  }
  getMonthlySummary(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/monthly-summary`);
  }

  getCategorySummary(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/category-summary`);
  }
}
