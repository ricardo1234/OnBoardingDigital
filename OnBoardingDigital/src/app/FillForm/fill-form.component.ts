import { Component, ViewChild, ViewChildren } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormService } from '../services/form.service';
import { FormResponse, FormSectionResponse } from '../Dtos/formResponse';
import { NbStepperComponent, NbToggleComponent } from '@nebular/theme';
import { SubscriptionRequest, subscriptionAnswerRequest } from '../Dtos/subscriptionRequest';
import { SubscriptionService } from '../services/subscription.service';

@Component({
  selector: 'app-fill-form',
  templateUrl: './fill-form.component.html',
  styleUrls: ['./fill-form.component.css']
})
export class FillFormComponent {
  id!: string | null;
  loading = true;
  error = false;
  formResponse!: FormResponse;
  sections!:  FormSectionResponse[];
  @ViewChild("sectionSteps") stepper!: NbStepperComponent;
  @ViewChild("chkRgpd") chkRgpd!: NbToggleComponent;
  emailStatus: string = "danger";

  subscriptionRequest! : SubscriptionRequest;

  formData: FormData;

  constructor(private activatedRoute: ActivatedRoute, private formService: FormService, private subscriptionService: SubscriptionService) {  this.formData = new FormData();}

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      this.formService.getForm(this.id).subscribe({
        next: (response) => {
          this.formResponse = response;
          this.formResponse.sections.forEach((section) => {
            if(!section.canRepeat) return;
            section.answersIndex = [1];
          });
          this.createForm();
        },
        error: () => {
          this.error = true;
          this.loading = false;
        }
     });
    }
  }

  addAnswerToSection(sectionId : string){
    let section = this.sections.find((section) => section.id == sectionId);
    if(section == undefined)
      return;

    section.answersIndex.push(section.answersIndex.length);

    this.createForm();
  }
  removeAnswerToSection(sectionId : string){
    let section = this.sections.find((section) => section.id == sectionId);
    if(section == undefined)
      return;

      if(section.answersIndex.length == 1) return;

    section.answersIndex.splice(section.answersIndex.length-1);

    this.createForm();
  }

  createForm() {
    this.sections = [];

    this.createSectionsRecursive(this.formResponse.firstSection);

    this.loading = false;
  }

  createSectionsRecursive(sectionId : string | null | undefined){
    let section = this.formResponse.sections.find((section) => section.id == sectionId);

    if(section == undefined)
      return;

    section.fields.sort((a, b) => a.order - b.order);
    this.sections.push(section);

    let fields = section.fields.filter(
      (field) => (field.type.id == 79 || field.type.id == 67) &&
      (field.choiceSettings?.nextSection != null || field.optionsSettings?.options.find((option) => option.nextSection != null) != undefined));

    if(fields.length == 0){
      this.createSectionsRecursive(section.defaultNextSection);
      return;
    }

    if (this.subscriptionRequest == null || this.subscriptionRequest == undefined){
      return;
    }

    for (let index = 0; index < fields.length; index++) {
      const element = fields[index];
      let answer = this.subscriptionRequest.answers.find((answer) => answer.fieldId == element.id);
      if(answer != undefined){
        var nextSection = element.type.id == 67 ? element.choiceSettings?.nextSection : element.optionsSettings?.options.find((option) => option.value == answer?.answer)?.nextSection;
        this.createSectionsRecursive(nextSection);
        return;
      }
    }
  }



  changeSection(id: string) {
    //get form group by id
    let hasError = false;
    let submittedSection = this.sections.find((section) => section.id == id);
    if(submittedSection == undefined)
      return;

    submittedSection?.fields.forEach((field) => {

      if(!submittedSection?.canRepeat){
        if(this.saveField(field.id, field.type.id)){
          hasError = true;
        }
        return;
      }

      for (let index = 0; index < submittedSection?.answersIndex.length; index++) {
        let field_ID = field.id + '_' + (index);
        if(this.saveField(field_ID, field.type.id))
          hasError = true;
      }
    });

    if(!hasError) {
      this.createForm();
      this.stepper.next();
    }
  }

  saveField(id: string, type : number) : boolean {
    let hasError = false;

    let index = -1;

    if(id.includes('_')){
      let fieldId = id.split('_')[0];
      let answerIndex = parseInt(id.split('_')[1]);
      index = this.subscriptionRequest.answers.findIndex((ans) => ans.fieldId == fieldId && ans.index == answerIndex+1);
    }else{
      index = this.subscriptionRequest.answers.findIndex((ans) => ans.fieldId == id);
    }

    if(index != -1){
      this.subscriptionRequest.answers.splice(index, 1);
    }

    switch (type) {
      case 84:
        if(!this.saveTextField(id))
          hasError = true;
        break;
      case 79:
        if(!this.saveOptionField(id))
          hasError = true;
        break;
      case 70:
        if(!this.saveFileField(id))
          hasError = true;
        break;
        case 67:
          if(!this.saveChoiceField(id))
            hasError = true;
          break;
      default:
          break;
    }
    return hasError;
  }

  saveChoiceField(id: string) : boolean {
    var control = document.getElementById(id);
    if(control == null){
      return false;
    }

    if (control.getAttribute("ng-reflect-status") != "success") {
      return false;
    }
    let valueControl = control?.children[0].children[0]

    if(valueControl == null){
      return false;
    }

    let value = valueControl.getAttribute('aria-checked') ?? "false";
    this.addAnswer(id, value);
    return true;
  }
  saveTextField(id: string) : boolean {
    var control = document.getElementById(id)
    if(control == null){
      return false;
    }

    if (control.getAttribute("ng-reflect-status") != "success") {
      return false;
    }

    let value = ( control as HTMLInputElement).value;
    this.addAnswer(id, value);
    return true;
  }
  saveOptionField(id: string) : boolean {
    var control = document.getElementById(id);
    if(control == null){
      return false;
    }
    if (control.getAttribute("ng-reflect-status") != "success") {
      return false;
    }
    let value = (control.children[0].children[0] as HTMLElement).innerText;
    this.addAnswer(id, value);
    return true;
  }

  saveFileField(id: string) : boolean {
    var control = document.getElementById(id)
    if(control == null){
      return false;
    }

    if (control.getAttribute("ng-reflect-status") != "success") {
      return false;
    }
    var file = (control as HTMLInputElement)?.files;
    if(file == null){
      return false;
    }

    this.formData.append(id, file[0]);

    let value = file[0].name;
    this.addAnswer(id, value);
    return true;
  }

  addAnswer(id: string, value: string) : void{
    if(!id.includes('_')){
      this.subscriptionRequest.answers.push({fieldId: id, answer: value, index: 1} as subscriptionAnswerRequest);
      return;
    }

    let fieldId = id.split('_')[0];
    let index = parseInt(id.split('_')[1]);
    this.subscriptionRequest.answers.push({fieldId: fieldId, answer: value, index: index+1} as subscriptionAnswerRequest);
  }

  saveSubscription() : void {
    this.loading = true;

    this.subscriptionService.save(this.subscriptionRequest, this.formData).subscribe({
      next: () => {
        this.formData.delete('subscription');
        this.loading = false;
      },
      error: () => {
        this.error = true;
        this.loading = false;
        this.formData.delete('subscription');
      }
    });
  }


   //#region Identification
   validateIdentification(){
    if(document.getElementById("emailIdentification")?.getAttribute("ng-reflect-status") != "success")
      return;
    if(!this.chkRgpd.checked)
      return;

    this.subscriptionRequest = new SubscriptionRequest();
    this.subscriptionRequest.formId = this.id!;
    this.subscriptionRequest.email = (document.getElementById("emailIdentification") as HTMLInputElement).value;
    this.subscriptionRequest.answers = [];

    this.stepper.next();
  }
  changeEmail(){
    let value = (document.getElementById("emailIdentification") as HTMLInputElement).value;
    if(value == "" || value == undefined || value == null){
      this.emailStatus = "danger";
      return;
    }

    let regex = new RegExp(/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/);
    if(!regex.test(value)){
      this.emailStatus = "danger";
    }else{
      this.emailStatus = "success";
    }
  }
  changeStatusRGPD(){
    if(this.chkRgpd.checked)
      this.chkRgpd.status = "success";
    else
      this.chkRgpd.status = "danger";
  }
  //#endregion
}


