import { Injectable } from '@angular/core';
import { FormResponse } from '../Dtos/formResponse';
import { environment } from '../../environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  constructor(private http: HttpClient) { }

  getForm(id: string | null) : Observable<FormResponse> {
    return this.http.get<FormResponse>(environment.API_URL + '/form/' + id);
  }

  existsForm(id: string | null) : Observable<boolean> {
    return this.http.get<boolean>(environment.API_URL + '/form/exists/' + id);
  }
}
