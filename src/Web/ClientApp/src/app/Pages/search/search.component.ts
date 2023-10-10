import { Component } from '@angular/core';
import { ProductsClient } from '../../web-api-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  products: any[];
  constructor(private service: ProductsClient, private router: Router) { }
  ngOnInit() {
    console.log(this.router.url)
    let query: any = this.router.url.split('/search/');
    let search: string = query[1];
    console.log("search1: ", search);
    //this.router.navigate(["/search/", search]);
    //this.router.navigateByUrl("/search/${search}")
    this.service.getSearchProducts(search).subscribe(res => {
      console.table(res);
      this.products = res;
    })
  }
}
