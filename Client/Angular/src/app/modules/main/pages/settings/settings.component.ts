import { Component } from '@angular/core';
import { AccountService } from '../../../../core/services/account.service';
import { AuthenticationService } from '../../../../core/services/authentication.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent {

  constructor(private accountService: AccountService, private authenticationService: AuthenticationService) {

  }
  
  logout() {
    this.accountService.postApiAccountLogOut().subscribe(response => {
      this.authenticationService.logout('/account/login')
    })
  }

}
