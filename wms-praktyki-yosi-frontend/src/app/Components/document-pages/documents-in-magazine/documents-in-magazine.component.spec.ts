import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentsInMagazineComponent } from './documents-in-magazine.component';

describe('DocumentsInMagazineComponent', () => {
  let component: DocumentsInMagazineComponent;
  let fixture: ComponentFixture<DocumentsInMagazineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DocumentsInMagazineComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DocumentsInMagazineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
