import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FieldInformationTypeComponent } from './field-information-type.component';

describe('FieldInformationTypeComponent', () => {
  let component: FieldInformationTypeComponent;
  let fixture: ComponentFixture<FieldInformationTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldInformationTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldInformationTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
