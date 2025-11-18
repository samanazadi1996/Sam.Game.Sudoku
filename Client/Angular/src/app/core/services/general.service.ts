import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { HotToastService } from '@ngneat/hot-toast';

@Injectable({
  providedIn: 'root',
})
export class GeneralService {
  location?: any[] = [];
  constructor(public toast: HotToastService, private titleService: Title) { }

  setLocation(pages: any[]) {
    this.location = pages

    this.titleService.setTitle(pages[pages.length - 1]);

  }


  isSuccess(response: any): boolean {
    if (response.errors) {
      var apiErrors: Array<String> = [];
      for (let index = 0; index < response.errors.length; index++) {
        apiErrors.push(response.errors[index].description);
      }
      this.toast.error(apiErrors.join('<br>'));
      console.log(apiErrors.join('<br>'));
    }
    return response.success;
  }


  getErrors(response: any): Array<String> {
    var apiErrors: Array<String> = [];

    if (response.errors) {
      for (let index = 0; index < response.errors.length; index++) {
        apiErrors.push(response.errors[index].description);
      }
    }

    return apiErrors;
  }
  queryParams(params: any): string {
    const queryParams = new URLSearchParams();

    Object.keys(params).forEach((key) => {
      if (
        params[key] != null &&
        params[key] != '' &&
        params[key] != undefined
      ) {
        queryParams.append(key, params[key]);
      }
    });

    return queryParams.toString();
  }
}
