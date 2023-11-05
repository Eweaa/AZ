import { ChangeDetectorRef, Component, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { Product, ProductsClient } from '../../web-api-client';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent {
  products: Product[];
  constructor(private service: ProductsClient, private router: Router, private cdr: ChangeDetectorRef, private route: ActivatedRoute) { }
  ngOnInit() {
    console.log(this.router.url)
    let query: any = this.router.url.split('/search/');
    let search: string = query[1];
    console.log("search1: ", search);
    //this.products = this.route.params.pipe(
    //  map(params => params.s),
    //  map(s => this.service.getSearchProducts(search))
    //);
    this.service.getSearchProducts(search).subscribe(res => {
      this.products = res;
      this.cdr.detectChanges();
    })
  }
}
