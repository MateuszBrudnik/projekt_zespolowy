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
  getIncomeExpenseSummary(startDate: Date, endDate: Date, userId: string): Observable<any> {
    let startDate1 = startDate.toISOString();
    let endDate1 = endDate.toISOString();
    return this.http.get(`${this.apiUrl}/summary`, { params: { startDate1, endDate1, userId } });
  }

  getSpendingTrends(startDate: Date, endDate: Date, userId: string): Observable<any> {
    let startDate1 = startDate.toISOString();
    let endDate1 = endDate.toISOString();
    return this.http.get(`${this.apiUrl}/trends`, { params: { startDate1, endDate1, userId } });
  }

  getCategoryWiseExpenses(startDate: Date, endDate: Date, userId: string): Observable<any> {
    let startDate1 = startDate.toISOString();
    let endDate1 = endDate.toISOString();
    return this.http.get(`${this.apiUrl}/categories`, { params: { startDate1, endDate1, userId } });
  }

}
