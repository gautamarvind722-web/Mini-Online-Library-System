import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private baseUrl = 'https://localhost:7107/api/auth';
  private currentUserSubject = new BehaviorSubject<any>(null);
  private tokenKey = 'jwt_token';

  constructor(private http: HttpClient,private router:Router) {
    const savedToken = localStorage.getItem(this.tokenKey);
    if (savedToken) {
      const decoded = decodeJwt(savedToken);
      if (decoded) this.currentUserSubject.next({ ...decoded, token: savedToken });
    }
  }

  login(username: string, password: string) {
  return this.http.post<any>(`${this.baseUrl}/login`, { username, password })
    .pipe(tap(res => {
      const user = {
        token: res.token,
        userId: res.userId,
        role: res.role // make sure this exists
      };
      localStorage.setItem('currentUser', JSON.stringify(user));
      localStorage.setItem('jwt_token', res.token); 
      this.currentUserSubject.next(user);
    }));
}


  register(name: string, email: string, PasswordHash : string, role: string = 'User') {
    return this.http.post(`${this.baseUrl}/register`, { name, email, PasswordHash , role });
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem("currentUser");
    this.currentUserSubject.next(null);
    this.router.navigate(['/login'])
  }

  get currentUserValue() {
    return this.currentUserSubject.value;
  }

  get token(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.token;
  }
  
}
export function decodeJwt(token: string): any {
  try {
    const payload = token.split('.')[1];
    const decodedJson = atob(payload); // decode Base64
    return JSON.parse(decodedJson);
  } catch (e) {
    console.error('Invalid JWT token', e);
    return null;
  }
}

