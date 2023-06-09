import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FieldDateTimeTypeComponent } from './field-date-time-type.component';

describe('FieldDateTimeTypeComponent', () => {
  let component: FieldDateTimeTypeComponent;
  let fixture: ComponentFixture<FieldDateTimeTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldDateTimeTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldDateTimeTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
