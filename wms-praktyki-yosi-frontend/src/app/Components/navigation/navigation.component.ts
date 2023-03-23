import {
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { GlobalService } from '@services/global/global.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent {
  @ViewChild('navigation') myElement: any;

  title = 'wms-frontend';
  selected = localStorage.getItem('lang') || 'PL';
  isLoggedIn: boolean = false;

  isModeratorOrAdmin: boolean =
    localStorage.getItem('role') == 'Admin' ||
    localStorage.getItem('role') == 'Moderator';
  isAdmin: boolean = localStorage.getItem('role') == 'Admin';
  // User Admin Moderator

  constructor(private router: Router, private globalSrv: GlobalService) {
    this.router.events.subscribe((val) => {
      this.isLoggedIn = localStorage.getItem('token') == null ? false : true;
    });
  }
  saveToLocalStorage() {
    localStorage.setItem('lang', this.selected);
  }
  handleLogOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    window.location.reload();
  }

  ngOnInit(): void {
    this.globalSrv.role.subscribe((nextValue) => {
      this.isModeratorOrAdmin =
        nextValue == 'Admin' || nextValue == 'Moderator';
      this.isAdmin = nextValue == 'Admin';
    });
  }

  ngOnDestroy(): void {
    this.globalSrv.role.unsubscribe();
  }
}
