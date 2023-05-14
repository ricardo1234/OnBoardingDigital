import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FieldFileTypeComponent } from './field-file-type.component';

describe('FieldFileTypeComponent', () => {
  let component: FieldFileTypeComponent;
  let fixture: ComponentFixture<FieldFileTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FieldFileTypeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FieldFileTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
