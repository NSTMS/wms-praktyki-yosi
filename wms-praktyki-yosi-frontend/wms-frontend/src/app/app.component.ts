import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'wms-frontend';
  selected = localStorage.getItem('lang') || "PL";
  isLoggedIn : boolean = false;

  constructor(private router: Router){}

  saveToLocalStorage(){
    localStorage.setItem('lang', this.selected);
  }
  ngOnInit() {
    console.log(localStorage.getItem('token'));
    this.isLoggedIn = (localStorage.getItem('token') == null)? false : true;
    this.selected = localStorage.getItem('lang') as string;    
  }
  handleLogOut()
  {
    localStorage.removeItem('token')
    window.location.reload();
  }
}
