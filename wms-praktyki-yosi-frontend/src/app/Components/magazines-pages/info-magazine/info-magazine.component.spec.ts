import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoMagazineComponent } from './info-magazine.component';

describe('InfoMagazineComponent', () => {
  let component: InfoMagazineComponent;
  let fixture: ComponentFixture<InfoMagazineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [InfoMagazineComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(InfoMagazineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
