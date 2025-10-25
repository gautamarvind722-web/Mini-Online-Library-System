import { Component } from '@angular/core';
import { Book } from '../../services/book';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css',
})
export class AdminDashboard {
  // List of all books in the library
  books: any[] = [];

  // List of all borrow/return transactions
  transactions: any[] = [];

  // Model for adding a new book
  newBook: any = { title: '', author: '', description: '', filePath: '' };

  // Error message for API failures
  error = '';

  constructor(
    private bookService: Book,
    private http: HttpClient,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    // Load books and transactions when component initializes
    this.loadBooks();
    this.loadTransactions();
  }

  /** Fetch all books for the admin */
  loadBooks() {
    this.bookService.getAllBooks().subscribe({
      next: data => (this.books = data),
      error: err => (this.error = err.error),
    });
  }

  /** Fetch all borrow/return transactions */
  loadTransactions() {
    this.http.get<any[]>('https://localhost:7107/api/transactions').subscribe({
      next: data => (this.transactions = data),
      error: err => (this.error = err.error),
    });
  }

  /** Add a new book to the library */
  addBook() {
    this.bookService.addBook(this.newBook).subscribe({
      next: () => {
        // Reset newBook form after successful addition
        this.newBook = { title: '', author: '', description: '', filePath: '' };
        this.loadBooks(); // Refresh book list
      },
      error: err => (this.error = err.error),
    });
  }


  /**
   * Check if a borrowed book is overdue
   * @param borrowDate - The date when the book was borrowed
   * @returns true if borrowed more than 2 days ago, false otherwise
   */
  isOverdue(borrowDate: string): boolean {
    const borrow = new Date(borrowDate);
    const now = new Date();
    const diffDays = (now.getTime() - borrow.getTime()) / (1000 * 60 * 60 * 24);
    return diffDays > 2;
  }
}
