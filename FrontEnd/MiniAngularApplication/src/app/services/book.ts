import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Book {
  private baseUrl = 'https://localhost:7107/api/Books';

  constructor(private http: HttpClient) {}

  // Admin: Add a new book
  addBook(book: { title: string; author: string; description?: string; filePath?: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}`, book);
  }

  // Admin/User: Get all books (Admin) or available books (User)
  getAllBooks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}`);
  }

  getAvailableBooks(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/available`);
  }
}
