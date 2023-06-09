import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NbAccordionModule, NbButtonModule, NbCardModule, NbDatepickerModule, NbInputModule, NbLayoutModule, NbSelectModule, NbSpinnerModule, NbStepperModule, NbTimepickerModule, NbToggleModule } from '@nebular/theme';
import { FillFormComponent } from './fill-form.component';
import { LandingPageComponent } from '../LandingPage/landing-page.component';
import { FieldChoiceTypeComponent } from '../FormFieldTypes/FieldChoiceType/field-choice-type.component';
import { FieldDateTimeTypeComponent } from '../FormFieldTypes/FieldDateTimeType/field-date-time-type.component';
import { FieldFileTypeComponent } from '../FormFieldTypes/FieldFileType/field-file-type.component';
import { FieldInformationTypeComponent } from '../FormFieldTypes/FieldInformationType/field-information-type.component';
import { FieldOptionsTypeComponent } from '../FormFieldTypes/FieldOptionsType/field-options-type.component';
import { FieldTextTypeComponent } from '../FormFieldTypes/FieldTextType/field-text-type.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { NbEvaIconsModule } from '@nebular/eva-icons';


@NgModule({
  declarations: [
    FillFormComponent
    , FieldTextTypeComponent
    , FieldInformationTypeComponent
    , FieldChoiceTypeComponent
    , FieldOptionsTypeComponent
    , FieldFileTypeComponent
    , FieldDateTimeTypeComponent
  ],
  imports: [
    CommonModule,
    NbLayoutModule,
    NbEvaIconsModule,
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
    NbDatepickerModule,
    NbTimepickerModule
  ]
})
export class FillFormModule { }
