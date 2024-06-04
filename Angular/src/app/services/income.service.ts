import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Income } from '../models/income';

@Injectable({
  providedIn: 'root'
})
export class IncomeService {
  private apiUrl = 'http://localhost:5170/api/income';

  constructor(private http: HttpClient) { }

  getIncomes(): Observable<Income[]> {
    return this.http.get<Income[]>(this.apiUrl);
  }

  addIncome(income: Income): Observable<Income> {
    return this.http.post<Income>(this.apiUrl, income);
  }

  updateIncome(income: Income): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${income.id}`, income);
  }

  deleteIncome(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
