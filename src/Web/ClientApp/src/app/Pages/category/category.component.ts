import { Component } from '@angular/core';
import { ProductsClient } from '../../web-api-client';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent {
  products: any;
  constructor(private service: ProductsClient, private router: Router) { }
  ngOnInit() {
    console.log(this.router.url)
    let query: any = this.router.url.split('/category/');
    let category: string = query[1];
    this.service.getCategoryProducts(parseInt(category)).subscribe(res => {
      this.products = res;
      console.log(res);
    })
  }
}
