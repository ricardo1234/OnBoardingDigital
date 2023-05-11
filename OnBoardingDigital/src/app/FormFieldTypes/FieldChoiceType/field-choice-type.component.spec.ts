import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FieldChoiceTypeComponent } from './field-choice-type.component';

describe('FieldChoiceTypeComponent', () => {
  let component: FieldChoiceTypeComponent;
  let fixture: ComponentFixture<FieldChoiceTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldChoiceTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldChoiceTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
