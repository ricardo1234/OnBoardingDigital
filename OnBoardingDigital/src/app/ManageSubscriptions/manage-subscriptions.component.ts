import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SubscriptionService } from '../services/subscription.service';
import { AllSubscriptionResponse } from '../Dtos/allSubscriptionResponse';
import { NbWindowControlButtonsConfig, NbWindowService } from '@nebular/theme';
import { FormService } from '../services/form.service';
import { AllFormsResponse } from '../Dtos/allFormsResponse';
import { SubscriptionAnswerResponse, SubscriptionResponse } from '../Dtos/subscriptionResponse';
import { FormResponse } from '../Dtos/formResponse';
import { NumberInput } from '@angular/cdk/coercion';
import { HttpResponse } from '@angular/common/http';


interface FormSectionAnswer {
  id: string;
  name: string;
  order: number;
  answers: SubscriptionAnswer[];
}
interface SubscriptionAnswer {
  id: string;
  fieldName: string;
  value: string;
  order: number;
  index: number;
  isFile: boolean;
}

@Component({
  selector: 'app-manage-subscriptions',
  templateUrl: './manage-subscriptions.component.html',
  styleUrls: ['./manage-subscriptions.component.css']
})
export class ManageSubscriptionsComponent implements OnInit
{
  email: string = "";
  loading = true;
  error = false;
  subscriptions!: AllSubscriptionResponse[];
  forms!: AllFormsResponse[];
  selectedSubscription!: SubscriptionResponse;
  selectedForm!: FormResponse;

  currentSectionIndex : number = 0;
  allAnswers: FormSectionAnswer[] = [];


  @ViewChild('subscriptionAnswersShow', { read: TemplateRef }) subscriptionAnswersShow!: TemplateRef<HTMLElement>;

  constructor(private activatedRoute: ActivatedRoute, private windowService: NbWindowService,
    private subscriptionService: SubscriptionService, private formService: FormService
    ) { }

  ngOnInit(): void {
    this.email = this.activatedRoute.snapshot.paramMap.get('email')??"";

    if (!this.email) {
      this.error = true;
      this.loading = false;
      return;
    }

    this.bindData();
  }

  bindData(){
    this.formService.get().subscribe({
      next: (response) => {
        this.forms = response;
        this.subscriptionService.get(this.email).subscribe({
          next: (response) => {
            this.subscriptions = response;
            this.loading = false;
          },
          error: () => {
            this.error = true;
            this.loading = false;
          }
        });
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
    });
  }

  getFormName(id: string) : string {
    let form = this.forms.find((form) => form.id == id);
    if(form == undefined)
      return "";
    return form.name;
  }

  deleteSubscription(id: string) {
    this.loading = true;
    this.subscriptionService.delete(id).subscribe({
      next: () => {
        this.bindData();
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
     });
  }

  showDetails(id: string){
    this.loading = true;

    this.subscriptionService.getAnswers(id).subscribe({
      next: (response) => {
        this.selectedSubscription = response;
        this.formService.getForm(this.selectedSubscription.formId).subscribe({
          next: (response) => {
            this.selectedForm = response;
            this.currentSectionIndex = 0;
            this.allAnswers = [];
            this.mapSubscription(this.selectedForm.firstSection);
          },
          error: () => {
            this.error = true;
            this.loading = false;
          }
        });
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }
     });


  }

  mapSubscription(sectionId : string | null){
    let section = this.selectedForm.sections.find((section) => section.id == sectionId);
    if(section == undefined)
      return;

    let nextSection : string | null | undefined = section.defaultNextSection;

    let sectionAnswer : FormSectionAnswer = {
      id: section.id,
      name: section.name,
      order: this.currentSectionIndex,
      answers: []
    };

    this.currentSectionIndex++;

    section.fields.sort((a, b) => a.order - b.order);
    section.fields.forEach((field) => {
      var answerList = this.selectedSubscription.answers.filter((answer) => answer.fieldId == field.id);

      if(answerList == undefined)
        return;

      answerList.forEach((answer) => {
          sectionAnswer.answers.push({
            id: answer.id,
            fieldName: field.description,
            value: this.getAnswerValue(answer),
            order: field.order,
            index: answer.index,
            isFile: field.type.id == 70
          });

          if((field.type.id == 79 || field.type.id == 67) && nextSection == null){
            nextSection = field.type.id == 67 ? field.choiceSettings?.nextSection : field.optionsSettings?.options.find((option) => option.value == answer.optionsValue)?.nextSection;
          }
      });
    });

    if(sectionAnswer.answers.length > 0){
      sectionAnswer.answers.sort((a, b)=> {
        if (a.index === b.index){
          return a.order < b.order ? -1 : 1
        } else {
          return a.index < b.index ? -1 : 1
        }
      });
      this.allAnswers.push(sectionAnswer);
    }

    if(nextSection != null)
      this.mapSubscription(nextSection);
    else
      this.showDialog();
  }

  showDialog() {
    this.allAnswers.sort((a, b) => a.order - b.order);
    console.log(this.allAnswers);
    const buttonsConfig: NbWindowControlButtonsConfig = {
      minimize: false,
      maximize: false,
      fullScreen: true,
      close: true,
    };
    this.windowService.open(this.subscriptionAnswersShow, { title: `Detalhes da Subscrição`, buttons:buttonsConfig, hasBackdrop: true});
    this.loading = false;
  }

  downloadFile(subscriptionId: string, answerId: string, fileName: string){
    this.subscriptionService.downloadFile(subscriptionId, answerId).subscribe({
      next: (response) => {
        console.log(response);
        let blob = new Blob([response], { type: response.type });
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = fileName;
        link.click();
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }});
  }

  downloadReport(subscriptionId: string){
    this.subscriptionService.downloadReport(subscriptionId).subscribe({
      next: (response) => {
        let blob = new Blob([response], { type: response.type });
        var link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = 'report.pdf';
        link.click();
      },
      error: () => {
        this.error = true;
        this.loading = false;
      }});
  }

  getAnswerValue(answer: SubscriptionAnswerResponse) : string {
      if(answer.textValue != null)
        return answer.textValue;
      if(answer.choiceValue != null)
        return answer.choiceValue ? 'Sim' : 'Não';
      if(answer.optionsValue != null)
        return answer.optionsValue;
      if(answer.numberValue != null)
        return answer.numberValue.toString();
      if(answer.fileName != null)
        return answer.fileName;
      return '';
  }
  getFormFieldDescription(id: string) : string {
    let field = this.selectedForm.sections.flatMap((section) => section.fields).find((field) => field.id == id);
    if(field == undefined)
      return "";
    return field.description;
  }


}
