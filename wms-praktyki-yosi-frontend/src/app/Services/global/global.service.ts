import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  role = new BehaviorSubject(this.theRole);

  set theRole(value) {
    this.role.next(value); // this will make sure to tell every subscriber about the change.
    localStorage.setItem('role', value as string);
  }

  get theRole() {
    return localStorage.getItem('role');
  }
}
