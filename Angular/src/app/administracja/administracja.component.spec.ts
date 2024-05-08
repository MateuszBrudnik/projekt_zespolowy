import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministracjaComponent } from './administracja.component';

describe('AdministracjaComponent', () => {
  let component: AdministracjaComponent;
  let fixture: ComponentFixture<AdministracjaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministracjaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministracjaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
