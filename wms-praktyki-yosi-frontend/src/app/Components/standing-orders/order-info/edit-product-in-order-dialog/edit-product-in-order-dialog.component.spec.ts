import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditProductInOrderDialogComponent } from './edit-product-in-order-dialog.component';

describe('EditProductInOrderDialogComponent', () => {
  let component: EditProductInOrderDialogComponent;
  let fixture: ComponentFixture<EditProductInOrderDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditProductInOrderDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditProductInOrderDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
