import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../core/services/authentication.service';
import { environment } from '../../../environments/environment';
import { AccountService } from '../../core/services/account.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent implements OnInit {
  profileImage?: string;
  isMainMenuOpen = false;


  constructor(private authenticationService: AuthenticationService, private accountService: AccountService) {
  }
  ngOnInit(): void {
    var temp = this.authenticationService.getProfileImage()
    if ((temp + "").length < 10)
      this.profileImage = environment.serverUrl + `/profile-images/` + temp
    else
      this.profileImage = temp

  }

  toggleMainMenu() {
    this.isMainMenuOpen = !this.isMainMenuOpen;
  }

  logout() {
    this.toggleMainMenu()
    this.accountService.postApiAccountLogOut().subscribe(response => {      
      this.authenticationService.logout('/account/login')
    })
  }

  openStats() {
    this.toggleMainMenu()
  }

  openProfile() {
    this.toggleMainMenu()
  }
}
