import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../core/services/account.service';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { GeneralService } from '../../../../core/services/general.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {

  constructor(private accountService: AccountService, private authenticationService: AuthenticationService, private generalService: GeneralService) {

  }

    ngOnInit(): void {
    this.generalService.setButtonsState({
      backDisplay: true,
      profileDisplay: false,
      settingsDisplay: true,
      title: "Profile"
    })
  }
  
}
