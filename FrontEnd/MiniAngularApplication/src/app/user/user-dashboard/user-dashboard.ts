import { Component } from '@angular/core';
import { Book } from '../../services/book';
import { Borrow } from '../../services/borrow';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../auth/auth.service';

@Component({
  selector: 'app-user-dashboard',
  imports: [FormsModule, CommonModule],
  templateUrl: './user-dashboard.html',
  styleUrls: ['./user-dashboard.css'],
})
export class UserDashboard {
  books: any[] = [];
  borrowed: any[] = [];
  error = '';

  constructor(
    private bookService: Book,
    private borrowService: Borrow,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadBooks();
    this.loadBorrowed();
  }

  /** Helper: Get current logged-in user */
  private getCurrentUser() {
    const userStr = localStorage.getItem('currentUser');
    if (!userStr) return null;
    return JSON.parse(userStr);
  }

  /** Load all available books for the user */
  loadBooks() {
    this.bookService.getAvailableBooks().subscribe({
      next: data => (this.books = data),
      error: err => (this.error = err.error),
    });
  }

  /** Load borrowed books for the current user */
  loadBorrowed() {
    const user = this.getCurrentUser();
    if (!user?.userId) {
      this.error = 'User not logged in or invalid';
      return;
    }

    this.borrowService.getBorrowedBooks(user.userId).subscribe({
      next: data => (this.borrowed = data),
      error: err => (this.error = err.error),
    });
  }

  /** Borrow a book for the current user */
  borrowBook(bookId: number) {
    const user = this.getCurrentUser();
    if (!user?.userId) {
      this.error = 'User not logged in or invalid';
      return;
    }

    this.borrowService.borrowBook(bookId, user.userId).subscribe({
      next: () => {
        this.loadBooks();
        this.loadBorrowed();
      },
      error: err => (this.error = err.error),
    });
  }

  /** Return a borrowed book */
  returnBook(transactionId: number) {
    this.borrowService.returnBook(transactionId).subscribe({
      next: () => this.loadBorrowed(),
      error: err => (this.error = err.error),
    });
  }
}
