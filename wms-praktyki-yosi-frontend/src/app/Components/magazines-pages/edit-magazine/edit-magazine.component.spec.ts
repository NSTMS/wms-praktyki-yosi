import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMagazineComponent } from './edit-magazine.component';

describe('EditMagazineComponent', () => {
  let component: EditMagazineComponent;
  let fixture: ComponentFixture<EditMagazineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditMagazineComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EditMagazineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
