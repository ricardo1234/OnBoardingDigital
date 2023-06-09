import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LandingPageComponent } from '../LandingPage/landing-page.component';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { NbLayoutModule, NbCardModule, NbSpinnerModule } from '@nebular/theme';



@NgModule({
  declarations: [
    LandingPageComponent
  ],
  imports: [
    CommonModule,
    NbLayoutModule,
    NbEvaIconsModule,
    NbCardModule,
    NbSpinnerModule
  ]
})
export class LandingPageModule { }
