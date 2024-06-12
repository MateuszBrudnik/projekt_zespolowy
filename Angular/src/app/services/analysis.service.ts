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
  getReportSummary(startDate: Date, endDate: Date): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/summary?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`);
  }

  getExpenseTrends(startDate: Date, endDate: Date): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/trends?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`);
  }

  getCategoryExpenses(startDate: Date, endDate: Date): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/categories?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`);
  }

}
