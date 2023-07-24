import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { TimeOffRequestAllHistoryComponent, TimeOffRequestRecievedHistoryComponent, TimeOffRequestSentHistoryComponent, TimeOffRequestsModuleComponent } from './component';

const routes: Routes = [
  {
    path: '',
    component: TimeOffRequestsModuleComponent
  },
  {
    path: 'recieved-request-history/:number',
    component: TimeOffRequestRecievedHistoryComponent
  },
  {
    path: 'sent-request-history/:number',
    component: TimeOffRequestSentHistoryComponent
  },
  {
    path: 'all-request-history/:number',
    component: TimeOffRequestAllHistoryComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TimeOffRequestsRoutingModule { }