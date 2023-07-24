import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';

import { AllNonCompilanceDetailsComponent, MyNonCompilanceDetailsComponent, NonComplianceModuleComponent, RecievedNonCompilanceDetailsComponent, SentNonCompilanceDetailsComponent } from './component';

const routes: Routes = [
  {
    path: '',
    component: NonComplianceModuleComponent
  },
  {
    path: 'all-non-compliance/:number',
    component: AllNonCompilanceDetailsComponent
  },
  {
    path: 'sent-non-compliance/:number',
    component: SentNonCompilanceDetailsComponent
  },
  {
    path: 'recieved-non-compliance/:number',
    component: RecievedNonCompilanceDetailsComponent
  },
  {
    path: 'my-non-compliance/:number',
    component: MyNonCompilanceDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NonComplianceModuleRoutingModule { }
