import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderBasketComponent } from './header-basket.component';

describe('HeaderBasketComponent', () => {
  let component: HeaderBasketComponent;
  let fixture: ComponentFixture<HeaderBasketComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HeaderBasketComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderBasketComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
