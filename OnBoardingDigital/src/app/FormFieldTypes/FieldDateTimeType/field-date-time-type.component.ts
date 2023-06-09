import { Component, Input, NgModule, OnInit } from '@angular/core';
import { FormFieldDateTimeSettingsResponse } from 'src/app/Dtos/formResponse';


@Component({
  selector: 'field-date-time-type',
  template: `<div class="DynamicControl">
              <label>{{name}}</label>
              <input [nbDatepicker]="datepicker">
              <nb-datepicker #datepicker ></nb-datepicker>
            </div>`,
  styles: [
  ]
})
export class FieldDateTimeTypeComponent implements OnInit {
  @Input()
  id!: string;
  @Input()
  configuration!: FormFieldDateTimeSettingsResponse | null;
  @Input()
  name!: string;
  @Input()
  isRequired!: boolean;
  max!: Date |null;
  min!: Date |null;

  ngOnInit(): void {
    this.max = this.configuration?.isMaximumToday ? new Date() : ( this.configuration?.maximum ? this.configuration?.maximum : null);
    this.min = this.configuration?.isMinimumToday ? new Date() : ( this.configuration?.minimum ? this.configuration?.minimum : null);
  }

}
