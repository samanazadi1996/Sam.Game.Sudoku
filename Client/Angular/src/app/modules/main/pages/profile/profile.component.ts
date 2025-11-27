import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../core/services/account.service';
import { GeneralService } from '../../../../core/services/general.service';
import { environment } from '../../../../../environments/environment';
import { GetProfileResponseInterface } from '../../../../core/services/interfaces/get-profile-response-interface';
import { AuthenticationService } from '../../../../core/services/authentication.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  profile?: GetProfileResponseInterface;
  userNameEditMode = false
  nickNameEditMode = false

  constructor(
        private route: ActivatedRoute,
    private accountService: AccountService, private generalService: GeneralService, private authenticationService: AuthenticationService) {
  }

  ngOnInit(): void {
    this.generalService.setButtonsState({
      backDisplay: true,
      profileDisplay: false,
      settingsDisplay: true,
      title: "Profile"
    })
    this.loadData();
  }

  loadData() {
    var userName= this.route.snapshot.paramMap.get('userName')!;

    this.accountService.getApiAccountGetProfile(userName).subscribe(response => {
      this.profile = response.data
      if ((response.data.profileImage + "").length < 10)
        this.profile.profileImage = environment.serverUrl + `/profile-images/` + this.profile.profileImage
    })
  }
  closeUserNameEditMode() {
    this.userNameEditMode = false;
    this.loadData()
  }
  closeNickNameEditMode() {
    this.nickNameEditMode = false;
    this.loadData()
  }
  submitUpdateNickName() {
    this.accountService.postApiAccountChangeNickName({ nickName: this.profile?.nickName }).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.closeNickNameEditMode();
      }
    })
  }

  submitUpdateUserName() {
    this.accountService.postApiAccountChangeUserName({ userName: this.profile?.userName }).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.authenticationService.login(response);
        this.closeUserNameEditMode();
      }
    })
  }

}
