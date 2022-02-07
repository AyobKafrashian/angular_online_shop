import { Component, OnInit,ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';

@Component({
  selector: 'app-activate-account',
  templateUrl: './activate-account.component.html',
  styleUrls: ['./activate-account.component.scss']
})
export class ActivateAccountComponent implements OnInit {

  @ViewChild('sweetAlert') private sweetAlert: SwalComponent;


  isLoading = true;

  constructor(private activatedRouth: ActivatedRoute, private authService: AuthService, private routher: Router) { }

  ngOnInit(): void {
    this.authService.activateUser(this.activatedRouth.snapshot.params.activeCode).subscribe(res => {
      if (res.status === 'Success') {
        this.isLoading = false;
        this.routher.navigate(['Login']);

        this.sweetAlert.text = res.data['message'];
        this.sweetAlert.fire();
      }
    });
  }

}
