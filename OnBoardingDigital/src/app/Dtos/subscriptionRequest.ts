export class SubscriptionRequest {
  formId!: string;
  email!: string;
  answers!: subscriptionAnswerRequest[];
}

export class subscriptionAnswerRequest {
  fieldId!: string;
  index: number = 1;
  answer!: string;
}
