import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormFieldChoiceSettingsResponse } from 'src/app/Dtos/formResponse';

@Component({
  selector: 'field-choice-type',
  template: `<div class="DynamicControl"> <nb-toggle [id]="id" fullWidth [status]="status" (checkedChange)="validate()">{{name}}</nb-toggle> </div>`,
  styles: [
  ]
})
export class FieldChoiceTypeComponent implements OnInit {

  @Input()
  id!: string;
  @Input()
  configuration!: FormFieldChoiceSettingsResponse | null;
  @Input()
  name!: string;
  @Input()
  isRequired!: boolean;

  status: string = "primary";

  ngOnInit(): void {
    this.status = this.isRequired ? "danger" : "success";
  }

  validate(): void {
    if(this.isRequired)
    {
      if(document.getElementById(this.id)?.children[0].children[0].getAttribute('aria-checked') != "false")
      {
        this.status = "danger";
        return;
      }
    }
    this.status = "success";
  }
}
