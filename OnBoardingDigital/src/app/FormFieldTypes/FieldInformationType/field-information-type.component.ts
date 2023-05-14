import { Component, Input } from '@angular/core';
import { FormFieldInformationSettingsResponse } from 'src/app/Dtos/formResponse';

@Component({
  selector: 'field-information-type',
  template: `<div class="DynamicControl" [innerHTML]="configuration?.htmlValue" ></div>`,
  styles: [
  ]
})
export class FieldInformationTypeComponent{
  @Input()
  configuration!: FormFieldInformationSettingsResponse | null;

  constructor() { }

}
