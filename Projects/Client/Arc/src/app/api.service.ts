import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5233/api';  // Base URL for the API

  constructor(private http: HttpClient) { }

  // Method to perform login
  login(username: string, password: string): Observable<any> {

    console.log(username);
    console.log(password);
    const body = new HttpParams()
      .set('username', username)
      .set('password', password);

    return this.http.post(`${this.baseUrl}/Auth/login`, body.toString(), {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' } });
  }

  // Method to get data from /api/Home
  getHome(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/Home`);
  }

  // Method to get data from /api/Home/user
  getUsers(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/Home/user`);
  }

}
