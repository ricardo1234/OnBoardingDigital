import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FieldTextTypeComponent } from './field-text-type.component';

describe('FieldTextTypeComponent', () => {
  let component: FieldTextTypeComponent;
  let fixture: ComponentFixture<FieldTextTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldTextTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldTextTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
