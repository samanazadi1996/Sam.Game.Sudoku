import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../core/services/account.service';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { GeneralService } from '../../../../core/services/general.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss'
})
export class SettingsComponent implements OnInit {

  constructor(private accountService: AccountService, private authenticationService: AuthenticationService, private generalService: GeneralService) {

  }

  ngOnInit(): void {
    this.generalService.setButtonsState({
      homeDisplay: true,
      profileDisplay: true,
      settingsDisplay: false,
      title: "Settings"
    })
  }

  logout() {
    this.accountService.postApiAccountLogOut().subscribe(response => {
      this.authenticationService.logout('/account/login')
    })
  }

}
