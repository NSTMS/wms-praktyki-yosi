import { AfterViewInit, ChangeDetectorRef, Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements AfterViewInit {
  @ViewChild('navigation') myElement: any;

  title = 'wms-frontend';
  selected = localStorage.getItem('lang') || "PL";
  isLoggedIn: boolean = false;

  isModeratorOrAdmin : boolean = localStorage.getItem("role") == "Admin" || localStorage.getItem("role") == "Moderator"
  isAdmin : boolean = localStorage.getItem("role") == "Admin"
  // User Admin Moderator

  constructor(private router: Router,private cd: ChangeDetectorRef)
  {
    
    this.router.events.subscribe((val) => {
      this.isLoggedIn = (localStorage.getItem('token') == null) ? false : true;
    // console.log(this.isLoggedIn)
    })
  }
  saveToLocalStorage() {
    localStorage.setItem('lang', this.selected);
  }
  handleLogOut() {
    localStorage.removeItem('token');
    localStorage.removeItem("role");
    window.location.reload();
  }
  ngAfterViewInit() {
    // Call detectChanges on the ChangeDetectorRef to refresh the element
    this.cd.detectChanges();
    this.isModeratorOrAdmin = localStorage.getItem("role") == "Admin" || localStorage.getItem("role") == "Moderator"

  }
}
