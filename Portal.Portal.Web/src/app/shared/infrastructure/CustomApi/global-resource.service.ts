import { Injectable } from '@angular/core';

import { ResourceResponse } from '../PortalHttpClient';

@Injectable({
  providedIn: 'root'
})
export class GlobalResourceService {

 private globalResourceResponse: ResourceResponse;

  constructor() { }

  get resourceResponse(): ResourceResponse {
    if (!this.globalResourceResponse) {
      const resourceLayoutString = localStorage.getItem('resourceLayout');
      if (resourceLayoutString) {
        this.globalResourceResponse = JSON.parse(resourceLayoutString);
      }
    }
    return this.globalResourceResponse;
  }

}
