import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FieldOptionsTypeComponent } from './field-options-type.component';

describe('FieldOptionsTypeComponent', () => {
  let component: FieldOptionsTypeComponent;
  let fixture: ComponentFixture<FieldOptionsTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldOptionsTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldOptionsTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
