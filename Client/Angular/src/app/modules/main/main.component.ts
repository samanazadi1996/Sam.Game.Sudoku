import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../core/services/authentication.service';
import { environment } from '../../../environments/environment';
import { AccountService } from '../../core/services/account.service';
import { GeneralService } from '../../core/services/general.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  profileImage?: string;
  profileName?: string;
  gs: GeneralService;
  constructor(private authenticationService: AuthenticationService, private router: Router, private generalService: GeneralService, private location: Location) {
    this.gs = generalService
  }
  ngOnInit(): void {
    var temp = this.authenticationService.getProfileImage()
    if ((temp + "").length < 10)
      this.profileImage = environment.serverUrl + `/profile-images/` + temp
    else
      this.profileImage = temp

    this.profileName = this.authenticationService.getUserName()

  }

  gotoSettings() {
    this.router.navigate(['main', 'settings'])
  }

  gotoProfile() {
    this.router.navigate(['main', 'profile', this.profileName])
  }

  gotoHome() {
    this.router.navigate(['main'])
  }

  goBack() {
    this.location.back();
  }
  getBackDisplay() {
    return (location.pathname == '/main' || location.pathname.includes('create-game'))
  }


}
