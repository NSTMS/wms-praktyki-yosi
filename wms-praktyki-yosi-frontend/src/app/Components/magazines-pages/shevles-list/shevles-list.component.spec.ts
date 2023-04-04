import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShevlesListComponent } from './shevles-list.component';

describe('ShevlesListComponent', () => {
  let component: ShevlesListComponent;
  let fixture: ComponentFixture<ShevlesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShevlesListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShevlesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
