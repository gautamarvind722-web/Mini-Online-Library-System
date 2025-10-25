import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Navbar } from './navbar/navbar';
import { AuthGuard } from './guards/auth.guard';
import { Register } from './auth/register/register';
import { UserDashboard } from './user/user-dashboard/user-dashboard';
import { AdminDashboard } from './admin/admin-dashboard/admin-dashboard';
import { AdminGuard } from './guards/admin.guard';

export const routes: Routes = [
     { path: 'login', component: Login },
       { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'register', component: Register },
  { path: 'user', component: UserDashboard, canActivate: [AuthGuard] },
  { path: 'admin', component: AdminDashboard, canActivate: [AdminGuard] }
];
