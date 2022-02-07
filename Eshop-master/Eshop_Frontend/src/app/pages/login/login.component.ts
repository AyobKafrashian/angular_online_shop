import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CurrentUser } from 'src/app/DTOs/Account/CurrentUser';
import { LoginUserDTO } from 'src/app/DTOs/Account/LoginUserDTO';
import { AuthService } from 'src/app/services/auth.service';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { Router } from '@angular/router';
import {CookieService} from 'ngx-cookie-service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;

  @ViewChild('sweetAlert') private sweetAlert: SwalComponent;
  @ViewChild('sweetAlertSuccess') private sweetAlertSuccess: SwalComponent;

  constructor(private authService: AuthService, private router: Router , private cookieService: CookieService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      email: new FormControl(
        null,
        [
          Validators.required,
          Validators.email,
          Validators.maxLength(100)
        ]
      ),
      password: new FormControl(null,
        [
          Validators.required,
          Validators.maxLength(100)
        ]),
    });
  }

  submitLoginForm() {
    console.log(this.loginForm);

    if (this.loginForm.valid) {
      const loginData = new LoginUserDTO(this.loginForm.controls.email.value, this.loginForm.controls.password.value);

      this.authService.LoginUser(loginData).subscribe(res => {
        console.log(res);
        const currentUser = new CurrentUser(
          res.data.userId,
          res.data.firstName,
          res.data.lastName,
          res.data.address
        );

        if (res.status === "Success") {
          this.cookieService.set('eshop-cookie' , res.data.token , res.data.expireTime * 60);
          this.authService.setCurrentUser(currentUser);
          this.loginForm.reset();
          this.router.navigate(['/']);
          this.sweetAlertSuccess.text = res.data['message'];
          this.sweetAlertSuccess.fire();
        }
        if (res.status === "Error") {
          this.sweetAlert.text = res.data['message'];
          this.sweetAlert.fire();
        }
       else if (res.status === "NotFound") {
          this.sweetAlert.text = res.data['message'];
          this.sweetAlert.fire(); 
        }
      });

    }
  }

}