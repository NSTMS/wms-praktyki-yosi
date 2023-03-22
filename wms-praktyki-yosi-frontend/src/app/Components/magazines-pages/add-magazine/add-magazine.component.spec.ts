import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMagazineComponent } from './add-magazine.component';

describe('AddMagazineComponent', () => {
  let component: AddMagazineComponent;
  let fixture: ComponentFixture<AddMagazineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AddMagazineComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AddMagazineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
