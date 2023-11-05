import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Product, ProductsClient } from '../web-api-client';
import { SearchService } from '../Services/SearchService';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  products: Product[];
  constructor(private router: Router, private productService: ProductsClient) { }

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
    this.router.navigateByUrl(`/search/${this.query}`);
    this.productService.getSearchProducts(this.query).subscribe(res => {
      console.table(res)
      // this.service.updateSearchResult(res);
      window.location.reload();
    })
  }
}
