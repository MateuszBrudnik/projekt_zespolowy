import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Expense } from '../models/expense';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = 'http://localhost:5170/api/reports';

  constructor(private http: HttpClient) { }

  getFilteredReport(startDate: Date, endDate: Date, category: string): Observable<Expense[]> {
    return this.http.get<Expense[]>(`${this.apiUrl}?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}&category=${category}`);
  }

  getPdfReport(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/pdf`, { responseType: 'blob' });
  }

  getCsvReport(): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/csv`, { responseType: 'blob' });
  }
}
