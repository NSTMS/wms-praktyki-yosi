import { Component  } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'wms-frontend';
  selected = localStorage.getItem('lang') || "PL";
  isLoggedIn: boolean = false;

  isModeratorOrAdmin : boolean = localStorage.getItem("role") == "Admin" || localStorage.getItem("role") == "Moderator"
  isAdmin : boolean = localStorage.getItem("role") == "Admin"
  // User Admin Moderator

  constructor(private router: Router)
  {
    
    this.router.events.subscribe((val) => {
      this.isLoggedIn = (localStorage.getItem('token') == null) ? false : true;
    console.log(this.isLoggedIn)
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
}
