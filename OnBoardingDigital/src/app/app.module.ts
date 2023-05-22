import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './LandingPage/landing-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NbThemeModule, NbLayoutModule, NbCardModule, NbSpinnerModule, NbStepperModule, NbInputModule, NbButtonModule, NbToggleModule, NbSelectModule, NbAccordionComponent, NbAccordionModule, NbTreeGridModule, NbIconModule } from '@nebular/theme';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { HttpClientModule } from '@angular/common/http';
import { FillFormComponent } from './FillForm/fill-form.component';
import { FormsModule } from '@angular/forms';
import { FieldTextTypeComponent } from './FormFieldTypes/FieldTextType/field-text-type.component';
import { FieldInformationTypeComponent } from './FormFieldTypes/FieldInformationType/field-information-type.component';
import { FieldChoiceTypeComponent } from './FormFieldTypes/FieldChoiceType/field-choice-type.component';
import { FieldOptionsTypeComponent } from './FormFieldTypes/FieldOptionsType/field-options-type.component';
import { FieldFileTypeComponent } from './FormFieldTypes/FieldFileType/field-file-type.component';
import { ManageSubscriptionsComponent } from './ManageSubscriptions/manage-subscriptions.component';


@NgModule({
  declarations: [
    AppComponent
  , LandingPageComponent
  , FillFormComponent
  , FieldTextTypeComponent
  , FieldInformationTypeComponent
  , FieldChoiceTypeComponent
  , FieldOptionsTypeComponent
  , FieldFileTypeComponent
  , ManageSubscriptionsComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NbThemeModule.forRoot({ name: 'cosmic' }),
    NbLayoutModule,
    NbEvaIconsModule,
    NbIconModule,
    NbCardModule,
    NbSpinnerModule,
    NbStepperModule,
    NbInputModule,
    NbButtonModule,
    HttpClientModule,
    NbToggleModule,
    NbSelectModule,
    NbAccordionModule,
    FormsModule,
    NbTreeGridModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
