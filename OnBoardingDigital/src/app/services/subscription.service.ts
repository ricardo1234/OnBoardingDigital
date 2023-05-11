import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SubscriptionRequest } from '../Dtos/subscriptionRequest';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(private http: HttpClient) { }

  save(subscription: SubscriptionRequest, files: FormData) : Observable<boolean> {
    files.append('subscription', JSON.stringify(subscription));
    return this.http.post<boolean>(environment.API_URL + '/Subscription', files);
  }
}
