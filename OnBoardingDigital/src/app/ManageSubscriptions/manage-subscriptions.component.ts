import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SubscriptionService } from '../services/subscription.service';

@Component({
  selector: 'app-manage-subscriptions',
  templateUrl: './manage-subscriptions.component.html',
  styleUrls: ['./manage-subscriptions.component.css']
})
export class ManageSubscriptionsComponent implements OnInit
{
  email: string | null = "";
  loading = true;
  error = false;

  constructor(private activatedRoute: ActivatedRoute,private subscriptionService: SubscriptionService) { }

  ngOnInit(): void {
    this.email = this.activatedRoute.snapshot.paramMap.get('email');

    if (!this.email) {
      this.error = true;
      this.loading = false;
      return;
    }


  }
}
