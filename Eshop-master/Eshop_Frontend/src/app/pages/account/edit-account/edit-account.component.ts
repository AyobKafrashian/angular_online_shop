import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EditUserDTO } from 'src/app/DTOs/Account/EditUserDTO';
import { AuthService } from 'src/app/services/auth.service';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { CurrentUser } from 'src/app/DTOs/Account/CurrentUser';

@Component({
  selector: 'app-edit-account',
  templateUrl: './edit-account.component.html',
  styleUrls: ['./edit-account.component.scss']
})
export class EditAccountComponent implements OnInit {

  @ViewChild('sweetAlertSuccess') private sweetAlertSuccess: SwalComponent;

  public editUser: FormGroup;
  currentUser: CurrentUser;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.getCurrentUser().subscribe(res => {
      this.currentUser = res;

      this.editUser = new FormGroup({
        firstName: new FormControl(
          res.firstName,
          [
            Validators.required,
            Validators.maxLength(100)
          ]
        ),
        lastName: new FormControl(
          res.lastName,
          [
            Validators.required,
            Validators.maxLength(100)
          ]),
        address: new FormControl(
          res.address,
          [
            Validators.required,
            Validators.maxLength(500)
          ])
      });
    });
  }

  submitEditUser() {
    if (this.editUser.valid) {
      const user = new EditUserDTO(
        this.editUser.controls.firstName.value,
        this.editUser.controls.lastName.value,
        this.editUser.controls.address.value,
      );
      this.authService.editUserAccount(user).subscribe(res => {
        if (res.status === 'Success') {
          this.sweetAlertSuccess.text = res.data['message'];
          this.sweetAlertSuccess.fire();
          this.authService.setCurrentUser(new CurrentUser(
            this.currentUser.userId,
            user.firstName,
            user.lastName,
            user.address,
          ));
        }
      });
    }
    //کد زیر یعنی اگر فیلد ها خالی بودن و دکه رو زد فیلتر ها اعمال میشه
    else {
      this.editUser.markAllAsTouched();
    }
  }
}
