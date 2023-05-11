export class FormResponse {
  id!: string;
  name!: string;
  firstSection!: string;
  sections!: FormSectionResponse[];
}

export class FormSectionResponse {
  id!: string;
  order!: number;
  name!: string;
  canRepeat!: boolean;
  numberOfRepeats!: number;
  currentInstance!: number;
  defaultNextSection!: string  | null;
  answersIndex!: number[];
  fields!: FormFieldResponse[];
}

export class FormFieldResponse {
  id!: string;
  order!: number;
  required!: boolean;
  description!: string;
  type!: FormFieldTypeResponse;
  choiceSettings!: FormFieldChoiceSettingsResponse | null;
  fileSettings!: FormFieldFileSettingsResponse | null;
  numberSettings!: FormFieldNumberSettingsResponse | null;
  optionsSettings!: FormFieldOptionsSettingsResponse | null;
  textSettings!: FormFieldTextSettingsResponse | null;
  informationSettings!: FormFieldInformationSettingsResponse | null;
  dateTimeSettings!: FormFieldDateTimeSettingsResponse | null;
}

export class FormFieldTypeResponse {
  id!: number;
  description!: string;
}

export class FormFieldChoiceSettingsResponse {
  group!: string;
  nextSection!: string | null;
}

export class FormFieldFileSettingsResponse {
  extensions!: string[];
  maxSize!: number;
}

export class FormFieldNumberSettingsResponse {
  minimum!: number;
  maximum!: number;
  requiredDigits!: number;
}

export class FormFieldOptionsSettingsResponse {
  options!: FieldOptionObjectResponse[];
}
export class FieldOptionObjectResponse {
  value!: string;
  text!: string;
  nextSection!: string | null;
}

export class FormFieldTextSettingsResponse {
  charMaximum!: number;
  charMinimum!: number;
  validationExpression!: string;
}

export class FormFieldInformationSettingsResponse {
  htmlValue!: string;
}

export class FormFieldDateTimeSettingsResponse {
  hasTime!: boolean;
  hasDate!: boolean;
  isMinimumToday!: boolean;
  isMaximumToday!: boolean;
  minimum!: Date;
  maximum!: Date;
}
