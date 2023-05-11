import { Component, Input, OnInit } from '@angular/core';
import { FormFieldFileSettingsResponse } from 'src/app/Dtos/formResponse';

@Component({
  selector: 'field-file-type',
  template: `<div> <label>{{name}}</label><input type="file"
  class="form-control"
  id="{{id}}"
  [required]="isRequired"
  [accept]="'.' + configuration?.extensions?.join(', .')"
  nbInput
  fullWidth
  (change)="validate($event)"
  [status]="status"></div>`,
  styles: [
  ]
})
export class FieldFileTypeComponent implements OnInit {
  @Input()
  id!: string;
  @Input()
  configuration!: FormFieldFileSettingsResponse | null;
  @Input()
  name!: string;
  @Input()
  isRequired!: boolean;

  status: string = "primary";

  constructor() { }

  ngOnInit(): void {
    this.status = this.isRequired ? "danger" : "success";
  }

  validate(event: any): void {
    console.log(event.target.files[0].name);
    console.log(event.target.files[0]);


    this.status = "success";
  }
}
