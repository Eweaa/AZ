import { BehaviorSubject } from "rxjs";
import { Product } from "../web-api-client";

export class SearchService {
    constructor(){}
    private searchResult = new BehaviorSubject<Array<Product>>([]);
    serachRefreshListener = this.searchResult.asObservable();

    updateSearchResult(result: Array<Product>){
        this.searchResult.next(result); 
    }

}