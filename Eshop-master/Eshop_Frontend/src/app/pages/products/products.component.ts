import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FilterProductDTO } from 'src/app/DTOs/Products/FilterProductDTO';
import { ProductCategory } from 'src/app/DTOs/Products/ProductCategory';
import { ProductOrderBy } from 'src/app/DTOs/Products/ProductOrderBy';
import { ProductsService } from 'src/app/services/products.service';

declare function jqUiSlider();

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  filterProducts: FilterProductDTO = new FilterProductDTO(
    '', 0, 0, 1, 0, 0, 0, 6, 0, 1, null, [], []
  );
  pages: number[] = [];
  categories: ProductCategory[] = [];


  constructor(private productService: ProductsService, private activatedRouthe: ActivatedRoute, private routher: Router) { }

  //#region NgOnInit
  ngOnInit(): void {
    this.activatedRouthe.queryParams.subscribe(params => {
      let pageId = 1;
      if (params.pageId !== undefined) {
        pageId = parseInt(params.pageId, 0);
      }

      this.filterProducts.categories = params.categories ? params.categories : [];
      this.filterProducts.pageId = pageId;
      this.filterProducts.startPrice = params.startPrice ? params.startPrice : 0;
      this.filterProducts.endPrice = params.endPrice ? params.endPrice : 0;

      this.getProduct();

    });

    this.productService.getproductActiveCategories().subscribe(res => {
      if (res.status === 'Success') {
        this.categories = res.data;
        console.log(this.categories);
      }
    });

    jqUiSlider();
  }
  //#endregion

  //#region  Format Label For Material
  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + 'k';
    }

    return value;
  }
  //#endregion

  //#region Set Min Price
  setMinPrice(event: any) {
    this.filterProducts.startPrice = parseInt(event.value, 0);
  }
  //#endregion

  //#region Set Max Price
  setMaxPrice(event: any) {
    this.filterProducts.endPrice = parseInt(event.value, 0);
  }
  //#endregion

  //#region Filter Button
  filterButton() {
    this.routher.navigate(['products'], { queryParams: { categories: this.filterProducts.categories, startPrice: this.filterProducts.startPrice, endPrice: this.filterProducts.endPrice } });
  }
  //#endregion

  //#region Change Order
  changeOrder(event: any) {
    // console.log(event);
    console.log(this.filterProducts);
    this.getProduct();

    switch (event.target.value) {
      case ProductOrderBy.PriceAsc.toString():
        this.routher.navigate(['products'], {
          queryParams: {
            pageId: this.filterProducts.activePage,
            categories: this.filterProducts.categories,
            orderBy: 'priceAsc',
            startPrice: this.filterProducts.startPrice,
            endPrice: this.filterProducts.endPrice
          }
        });
        break;
      case ProductOrderBy.PriceDes.toString():
        this.routher.navigate(['products'], {
          queryParams: {
            pageId: this.filterProducts.activePage,
            categories: this.filterProducts.categories,
            orderBy: 'priceDes',
            startPrice: this.filterProducts.startPrice,
            endPrice: this.filterProducts.endPrice
          }
        });
        break;
    }
  }
  //#endregion

  //#region Filter Categories
  filterCategories(event: any) {
    const value = event.target.value;
    if (this.filterProducts.categories === undefined || this.filterProducts.categories === null) {
      this.filterProducts.categories = [];
    }
    if (event.target.checked) {
      this.filterProducts.categories.push(parseInt(value, 0));
      this.setCategoriesFilter();
    }
    else {
      this.filterProducts.categories = this.filterProducts.categories.filter(s => s !== parseInt(value, 0));
      this.setCategoriesFilter();
    }

    console.log(this.filterProducts.categories);

  }
  //#endregion

  //#region Set Categpries Filter
  setCategoriesFilter() {
    if (this.filterProducts.categories.length > 0) {
      this.routher.navigate(['products'], { queryParams: { categories: this.filterProducts.categories, startPrice: this.filterProducts.startPrice, endPrice: this.filterProducts.endPrice } });
    }
    else if (this.filterProducts.startPrice > 0) {
      this.routher.navigate(['products'], { queryParams: { startPrice: this.filterProducts.startPrice, endPrice: this.filterProducts.endPrice } });
    }
    else {
      this.routher.navigate(['products']);
    }
  }
  //#endregion

  //#region Set Pages
  setPage(page: number) {
    this.routher.navigate(['products'], { queryParams: { pageId: page, categories: this.filterProducts.categories, startPrice: this.filterProducts.startPrice, endPrice: this.filterProducts.endPrice  } })
  }
  //#endregion
  
  //Methode
  //#region Get Product
  getProduct() {
    this.productService.getFilteredProducts(this.filterProducts).subscribe(res => {
      this.filterProducts = res.data;
      if (res.data.title === null) {
        this.filterProducts.title = '';
      }
      this.pages = [];

      for (let i = this.filterProducts.startPage; i <= this.filterProducts.endPage; i++) {
        this.pages.push(i);
      }
    });
  }
  //#endregion
}