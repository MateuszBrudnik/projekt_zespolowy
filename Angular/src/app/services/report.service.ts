import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private apiUrl = 'http://localhost:5170/api/reports';

  constructor(private http: HttpClient) { }

  getReports(startDate?: Date, endDate?: Date): Observable<any[]> {
    let params = new HttpParams();
    if (startDate) {
      params = params.set('startDate', startDate.toISOString());
    }
    if (endDate) {
      params = params.set('endDate', endDate.toISOString());
    }
    return this.http.get<any[]>(this.apiUrl, { params });
  }

  exportPdf(startDate?: Date, endDate?: Date): Observable<Blob> {
    let params = new HttpParams();
    if (startDate) {
      params = params.set('startDate', startDate.toISOString());
    }
    if (endDate) {
      params = params.set('endDate', endDate.toISOString());
    }
    return this.http.get(`${this.apiUrl}/pdf`, { params, responseType: 'blob' });
  }

  exportCsv(startDate?: Date, endDate?: Date): Observable<Blob> {
    let params = new HttpParams();
    if (startDate) {
      params = params.set('startDate', startDate.toISOString());
    }
    if (endDate) {
      params = params.set('endDate', endDate.toISOString());
    }
    return this.http.get(`${this.apiUrl}/csv`, { params, responseType: 'blob' });
  }

  getSummary(startDate?: Date, endDate?: Date): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/summary?startDate=${startDate.toISOString()}&endDate=${endDate.toISOString()}`);
  }
}
