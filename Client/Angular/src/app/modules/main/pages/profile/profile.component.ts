import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../core/services/account.service';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { GeneralService } from '../../../../core/services/general.service';
import { environment } from '../../../../../environments/environment';
import { GetProfileResponseInterface } from '../../../../core/services/interfaces/get-profile-response-interface';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  profile?: GetProfileResponseInterface;
  constructor(private accountService: AccountService, private generalService: GeneralService) {
  }

  ngOnInit(): void {
    this.generalService.setButtonsState({
      backDisplay: true,
      profileDisplay: false,
      settingsDisplay: true,
      title: "Profile"
    })
    this.accountService.getApiAccountGetProfile().subscribe(response => {
      this.profile = response.data
      
      if ((response.data.profileImage + "").length < 10)
        this.profile.profileImage = environment.serverUrl + `/profile-images/` + this.profile.profileImage

    })

  }

}
