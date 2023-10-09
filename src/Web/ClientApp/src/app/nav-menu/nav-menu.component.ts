import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  constructor(private router: Router) { }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  query: string;

  search = () => {
    this.query = (<HTMLInputElement>document.getElementById("search"))?.value;
    this.router.navigate(["/search/", this.query])
  }
}
