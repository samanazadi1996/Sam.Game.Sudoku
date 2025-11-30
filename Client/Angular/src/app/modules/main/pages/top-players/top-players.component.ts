import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../core/services/user.service';
import { GeneralService } from '../../../../core/services/general.service';
import { Route, Router } from '@angular/router';
import { GetTopPlayersPagedListResponseInterface } from '../../../../core/services/interfaces/get-top-players-paged-list-response-interface';
import { environment } from '../../../../../environments/environment';

@Component({
  selector: 'app-top-players',
  templateUrl: './top-players.component.html',
  styleUrl: './top-players.component.scss'
})
export class TopPlayersComponent implements OnInit {

  data?: GetTopPlayersPagedListResponseInterface[];

  constructor(private userService: UserService, private generalService: GeneralService, private router: Router) {
  }

  ngOnInit(): void {
    this.generalService.setButtonsState({
      homeDisplay: true,
      profileDisplay: true,
      settingsDisplay: true,
      title: "Top-Players"
    })

    this.userService.getApiUserGetTopPlayersPagedList(1, 20).subscribe(response => {
      if (this.generalService.isSuccess(response)) {
        this.data = response.data;
        this.data!.forEach(element => {
              if ((element.profileImage + "").length < 10)
                element.profileImage = environment.serverUrl + `/profile-images/` + element.profileImage  
        });
      }
    })
  }

  gotoProfile(userName: string) {
    this.router.navigate(['main', 'profile', userName])
  }
}
