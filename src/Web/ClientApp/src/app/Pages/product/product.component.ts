import { Component } from '@angular/core';
import { ProductsClient } from '../../web-api-client';
import { Router } from '@angular/router';
import { SearchService } from 'src/app/Services/SearchService';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent {
  data: any;
  constructor(private productService: ProductsClient, private router: Router) { }
  ngOnInit() {
    let query: any = this.router.url.split('/product/');
    let product: string = query[1];
    console.log(product);
    // this.service.serachRefreshListener.subscribe(res => {
    //   console.log("this is the result from the service", res)
    // })
    this.productService.getProduct(Number(product)).subscribe(res => {
      console.log(res);
      this.data = res;
    })
  }
}
