import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PutbackDialogComponent } from './putback-dialog.component';

describe('PutbackDialogComponent', () => {
  let component: PutbackDialogComponent;
  let fixture: ComponentFixture<PutbackDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PutbackDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PutbackDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
