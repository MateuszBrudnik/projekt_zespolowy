import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Notification } from '../models/notification';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private apiUrl = 'http://localhost:5170/api/notification';

  constructor(private http: HttpClient) { }

  getNotifications(): Observable<Notification[]> {
    return this.http.get<Notification[]>(this.apiUrl);
  }

  getNotificationHistory(): Observable<Notification[]> {
    return this.http.get<Notification[]>(`${this.apiUrl}/history`);
  }

  subscribeToNotifications(subscription: PushSubscription): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/subscribe`, subscription);
  }
}
