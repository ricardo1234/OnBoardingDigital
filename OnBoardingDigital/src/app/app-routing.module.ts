import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from './LandingPage/landing-page.component';
import { FillFormComponent } from './FillForm/fill-form.component';
import { ManageSubscriptionsComponent } from './ManageSubscriptions/manage-subscriptions.component';

const routes: Routes = [
{ path: 'form/:id', component: LandingPageComponent },
{ path: 'form/:id/fill', component: FillFormComponent },
{ path: 'subscription/ByEmail/:email', component: ManageSubscriptionsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
