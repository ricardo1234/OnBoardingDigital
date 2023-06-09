import { Component, Input, OnInit } from '@angular/core';
import { FieldOptionObjectResponse, FormFieldOptionsSettingsResponse } from 'src/app/Dtos/formResponse';

@Component({
  selector: 'field-options-type',
  template: `<div class="DynamicControl">
              <label>{{name}}</label>
              <nb-select [placeholder]="name" [id]="id" fullWidth (selectedChange)="validate($event)" [status]="status">
                <nb-option *ngFor="let option of configuration?.options" [value]="option.value">{{option.text}}</nb-option>
              </nb-select>
            </div>`,
  styles: [
  ]
})
export class FieldOptionsTypeComponent implements OnInit {
  @Input()
  id!: string;
  @Input()
  configuration!: FormFieldOptionsSettingsResponse | null;
  @Input()
  name!: string;
  @Input()
  isRequired!: boolean;

  status: string = "danger";

  options!: FieldOptionObjectResponse[] | undefined;

  constructor() { }

  ngOnInit(): void {
    this.options = this.configuration?.options;

    if(this.isRequired)
      this.status = "danger";
    else
      this.status = "success";
  }

  validate($event : any): void {
    var control = document.getElementById(this.id);
    if(control == null){
      return;
    }

    control.setAttribute('value-id', $event ?? "");

    this.status = "success";
  }
}
