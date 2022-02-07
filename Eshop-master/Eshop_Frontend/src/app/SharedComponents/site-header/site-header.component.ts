import { Component, OnInit } from '@angular/core';
import { CurrentUser } from 'src/app/DTOs/Account/CurrentUser';
import { AuthService } from 'src/app/services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-site-header',
  templateUrl: './site-header.component.html',
  styleUrls: ['./site-header.component.scss']
})
export class SiteHeaderComponent implements OnInit {

  user: CurrentUser = null;

  constructor(private authService: AuthService, private cookieService: CookieService , private routher : Router) { }

  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(user => {
      this.user = user;
    });
  }

  logOutUser() {
    // this.authService.logOutUser().subscribe(res => {

    //   if (res.status === 'Success') {
    //     console.log('user is log out');
    //   }
    // });

    this.cookieService.delete('eshop-cookie');
    this.authService.setCurrentUser(null);
    this.routher.navigate(['/']);

  }

}
