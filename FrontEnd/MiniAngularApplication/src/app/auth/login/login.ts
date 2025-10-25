import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
   username = '';
  password = '';
  error = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.username, this.password).subscribe({
      next: user => {
        if (user.role === 'Admin') this.router.navigate(['/admin']);
        else this.router.navigate(['/user']);
      },
      error: err => this.error = err.error || 'Login failed'
    });
  }
  register(){
    this.router.navigate(["/register"])
  }
}
