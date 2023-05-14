import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from '@angular/router';
import { FormService } from '../services/form.service';

@Component({
  selector: 'app-landing-page',
  template: `<nb-layout [nbSpinner]="loading"
                nbSpinnerStatus="info"
                nbSpinnerSize="giant"
                nbSpinnerMessage="Estamos a verificar o formulário...">
                <nb-layout-column *ngIf="error" >
                  <nb-card status="danger">
                    <nb-card-header>Erro</nb-card-header>
                    <nb-card-body>
                      Ocorreu um erro ao carregar o formulário. Por favor tente novamente mais tarde, ou contacte o administrador.
                    </nb-card-body>
                  </nb-card>
                </nb-layout-column>
          </nb-layout>`,
  styles: [
  ]
})
export class LandingPageComponent implements OnInit {
  id!: string | null;
  loading = true;
  error = false;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private formService: FormService) { }

  ngOnInit(): void {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    if (this.id) {
      this.formService.existsForm(this.id).subscribe({
        next: () => {
          this.router.navigate([`/form/${this.id}/fill`]);;
        },
        error: () => {
          this.error = true;
          this.loading = false;
        }
     });
    }
  }
}
