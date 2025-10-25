import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Borrow {
  private baseUrl = 'https://localhost:7107/api/Books';

  constructor(private http: HttpClient) {}

  // Borrow a book
  borrowBook(bookId: number, userId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/borrow`, { bookId, userId });
  }

  // Return a borrowed book
  returnBook(transactionId: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/return`, { transactionId });
  }

  // Get all borrowed books by user
  getBorrowedBooks(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/borrowed/${userId}`);
  }
}
