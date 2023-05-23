import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SubscriptionRequest } from '../Dtos/subscriptionRequest';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';
import { AllSubscriptionResponse } from '../Dtos/allSubscriptionResponse';
import { SubscriptionResponse } from '../Dtos/subscriptionResponse';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) { }

  save(subscription: SubscriptionRequest, files: FormData) : Observable<boolean> {
    files.append('subscription', JSON.stringify(subscription));
    return this.http.post<boolean>(environment.API_URL + '/Subscription', files);
  }

  get(email: string) : Observable<AllSubscriptionResponse[]> {
    return this.http.get<AllSubscriptionResponse[]>(environment.API_URL + '/Subscription/ByEmail/' + email);
  }

  getAnswers(id: string) : Observable<SubscriptionResponse> {
    return this.http.get<SubscriptionResponse>(environment.API_URL + '/Subscription/' + id);
  }

  delete(id: string) : Observable<null> {
    return this.http.delete<null>(environment.API_URL + '/Subscription/' + id);
  }

  downloadFile(subscriptionId: string, answerId: string) : Observable<Blob> {
    return this.http.get(environment.API_URL + '/Subscription/' + subscriptionId + '/GetFile/' + answerId, {responseType: 'blob'});
  }
}
