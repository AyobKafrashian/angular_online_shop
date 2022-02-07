import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth.service";
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })

export class UserAuthGuard implements CanActivate {

    constructor(private authService:AuthService,private routher: Router){}
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        return this.authService.isAuthenticated().then(res=>{
            if(res){
                return true;
            }
            else{
                this.routher.navigate(['Login']);
            }
            return false;
        });
    }
}