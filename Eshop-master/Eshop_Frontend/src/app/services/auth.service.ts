import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { CurrentUser } from '../DTOs/Account/CurrentUser';
import { EditUserDTO } from '../DTOs/Account/EditUserDTO';
import { ICheckUserAuthResult } from '../DTOs/Account/ICheckUserAuthResult';
import { ILoginUserAccount } from '../DTOs/Account/ILoginUserAccount';
import { LoginUserDTO } from '../DTOs/Account/LoginUserDTO';
import { RegisterUserDTO } from '../DTOs/Account/RegisterUserDTO';
import { IResponseResult } from '../DTOs/Common/IResponseResult';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggesIn = false;

  private currentUser: BehaviorSubject<CurrentUser> = new BehaviorSubject<CurrentUser>(null);

  constructor(private http: HttpClient) { }

  setCurrentUser(user: CurrentUser): void {
    this.currentUser.next(user);
    if (user !== null) {
      this.loggesIn = true;
    }
    else {
      this.loggesIn = false;
    }
  }

  isAuthenticated() {
    const promis = new Promise((resolve, rejects) => {
      resolve(this.loggesIn);
    });

    return promis;
  }

  getCurrentUser(): Observable<CurrentUser> {
    return this.currentUser;
  }

  registerUser(registerData: RegisterUserDTO): Observable<any> {
    return this.http.post<any>('/account/register', registerData);
  }

  LoginUser(loginUserDTO: LoginUserDTO): Observable<ILoginUserAccount> {
    return this.http.post<ILoginUserAccount>('/account/login', loginUserDTO);
  }

  checkUserAuth(): Observable<ICheckUserAuthResult> {
    return this.http.post<ICheckUserAuthResult>("/account/check-auth", null);
  }

  logOutUser(): Observable<any> {
    return this.http.get('/account/sign-out');
  }

  activateUser(emailActiveCode: string): Observable<any> {
    return this.http.get('/account/activate-account/' + emailActiveCode);
  }

  editUserAccount(user: EditUserDTO): Observable<IResponseResult<any>> {
    return this.http.post<IResponseResult<any>>('/account/edit-user/' , user);
  }

}
