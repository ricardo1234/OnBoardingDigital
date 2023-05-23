export class SubscriptionResponse{
  id!: string;
  email!: string;
  formId!: string;
  createdAtUtc!: Date;
  answers!: SubscriptionAnswerResponse[];
}

export class SubscriptionAnswerResponse{
  id!: string;
  fieldId!: string;
  index!: number;
  textValue!: string | null;
  choiceValue!: boolean | null;
  optionsValue!: string | null;
  numberValue!: number | null;
  fileName!: string | null;
}


