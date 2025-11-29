import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../core/services/user.service';
import { GeneralService } from '../../../../core/services/general.service';
import { GetTopUsersPagedListResponseInterface } from '../../../../core/services/interfaces/get-top-users-paged-list-response-interface';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-top-users',
  templateUrl: './top-users.component.html',
  styleUrl: './top-users.component.scss'
})
export class TopUsersComponent implements OnInit {

  data?: GetTopUsersPagedListResponseInterface[];

  constructor(private userService: UserService, private generalService: GeneralService, private router: Router) {
  }

  ngOnInit(): void {
    this.generalService.setButtonsState({
      homeDisplay: true,
      profileDisplay: true,
      settingsDisplay: true,
      title: "Top-Players"
    })

    this.userService.getApiUserGetTopUsersPagedList(1, 100).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.data = response.data;
      }
    })
  }

  gotoProfile(userName: string) {
    this.router.navigate(['main', 'profile', userName])
  }
}
