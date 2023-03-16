import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { ReplaySubject } from "rxjs";
import { Globals } from "../globals";
import { User } from "../_models/User";


import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService extends BaseService {

    public currentUserSource = new ReplaySubject(1); 
    currentUser$ = this.currentUserSource.asObservable();
    
    setCurrentUser(){
        // console.log("User: ", user);
        // user.roles = [];
        // const roles = JSON.parse(window.atob(this.authToken.split('.')[1])).role;
        // console.log("Roles: ", roles);
        // Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
        // localStorage.setItem('user', JSON.stringify(user));
        this.currentUserSource.next(this.userFromStorage);
        console.log("Current User Source: ", this.currentUserSource);
      }

    get accessibleModulesOfUser(): string[] {
        const payLoad = JSON.parse(window.atob(this.authToken.split('.')[1]));
        const accessibleModules = payLoad.AccessibleModules.split(",");

        return accessibleModules;
    }

    get authToken(): any {
        return localStorage.getItem(this.globals.TOKEN_NAME);
    }
    
    set authToken(val: string) {
        localStorage.setItem(this.globals.TOKEN_NAME, val);
    }

    get userFromStorage(): any {
        return localStorage.getItem(this.globals.USER);
    }

    get userObjectFromStorage(): any {
        return JSON.parse(this.userFromStorage);
    }
    
    set userToStorage(val: string) {
        localStorage.setItem(this.globals.USER, val);
    }

    get userID(): number {
        const payLoad = JSON.parse(window.atob(this.authToken.split('.')[1]));
        const userId = payLoad.UserGuid;

        return +userId;
    }

    get userName(): string {
        const payLoad = JSON.parse(window.atob(this.authToken.split('.')[1]));

        return payLoad.unique_name;
    }
    // *********************************For Studying purposes only**************************************
    get payLoad(): any {
        return JSON.parse(window.atob(this.authToken.split('.')[1]));
    }

    get atob(): any {
        return window.atob(this.authToken.split('.')[1]);
    }
    // *************************************************************************************************
    constructor(
      private globals: Globals,
      public override http: HttpClient) {
      super(http);
  }

    destroyAuthToken() {
        localStorage.removeItem(this.globals.TOKEN_NAME);
    }

    destroyUserObjectFromStorage() {
        localStorage.removeItem(this.globals.USER);
    }

    setCurrentUserToNull(){
        console.log("Setting current user to null")
        this.currentUserSource.next(null);
    }

    getAccessibleModulesOfUser(id: number) {
        return this.http.get(this.API_URL + '/Auth/getAccessibleModulesOfUser/' + id);
    }

    login(formData: any) {
        return this.http.post(this.API_URL + '/Auth/login', formData, { responseType: 'text' });
    }

    register(formData: any) {
        return this.http.post(this.API_URL + '/Auth/register', formData, { responseType: 'text' });
    }

    // userHasAccessToModule(accessibleModules: string[], moduleToCheck: string): boolean {
    //     const modules = accessibleModules.map(module => module.toUpperCase());

    //     return modules.includes(moduleToCheck.toUpperCase());
    // }

    userHasAccessToModule(moduleToCheck: string): boolean {
        const modules = this.accessibleModulesOfUser.map(module => module.toUpperCase());

        return modules.includes(moduleToCheck.toUpperCase());
    }

    // validateToken() {
    //     return this.http.get(this.API_URL + '/Auth/validateToken');
    // }
}