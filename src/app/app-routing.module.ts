import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: '',component: HomeComponent
  },
  {
    path: 'dashboard',component: DashboardComponent
  },
  {
    path: 'register',component: RegisterComponent
  },
  {
    path: 'login',component: LoginComponent
  },
  {
    path: 'edit-profile', canActivate: [AuthGuard], component: ProfileComponent, data: { module: 'Profile'}
  },
  {
    path: 'edit-profile/:id', canActivate: [AuthGuard], component: ProfileComponent, data: { module: 'Profile'}
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
