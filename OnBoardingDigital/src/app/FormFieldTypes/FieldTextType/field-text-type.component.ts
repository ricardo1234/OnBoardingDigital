import { Component, Input, OnInit } from '@angular/core';
import { FormFieldTextSettingsResponse } from 'src/app/Dtos/formResponse';

@Component({
  selector: 'field-text-type',
  template: `<div class="DynamicControl">
  <label>{{name}}</label>
  <input type="text"
  pattern="{{configuration?.validationExpression}}"
  placeholder="{{name}}"
  class="form-control"
  id="{{id}}"
  required="isRequired"
  nbInput
  fullWidth
  (change)="validate()"
  [status]="status"></div>`,
  styles: [
  ]
})
export class FieldTextTypeComponent implements OnInit
{
  @Input()
  id!: string;
  @Input()
  configuration!: FormFieldTextSettingsResponse | null;
  @Input()
  name!: string;
  @Input()
  isRequired!: boolean;

  status: string = "primary";

  constructor() { }

  ngOnInit(): void {
    this.status = this.isRequired ? "danger" : "success";
  }

  validate(): void {
    var value = (document.getElementById(this.id) as HTMLInputElement).value;

    if(this.configuration?.charMaximum)
    {
      if(value.length > this.configuration.charMaximum){
        this.status = "danger";
        return;
      }
    }

    if(this.configuration?.charMinimum)
    {
      if(value.length < this.configuration.charMinimum){
        this.status = "danger";
        return;
      }
    }

    if(this.configuration?.validationExpression){
      let regex = new RegExp(this.configuration.validationExpression);
      if(!regex.test((document.getElementById(this.id) as HTMLInputElement).value)){
        this.status = "danger";
      }
    }

    this.status = "success";
  }
}
