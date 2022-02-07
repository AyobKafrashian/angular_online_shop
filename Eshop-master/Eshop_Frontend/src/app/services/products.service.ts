import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IResponseResult } from '../DTOs/Common/IResponseResult';
import { AddProductComment } from '../DTOs/Products/AddProductComment';
import { FilterProductDTO } from '../DTOs/Products/FilterProductDTO';
import { Product } from '../DTOs/Products/Product';
import { ProductCategory } from '../DTOs/Products/ProductCategory';
import { ProductCommentDTO } from '../DTOs/Products/ProductCommentDTO';
import { ProductDetailDTO } from '../DTOs/Products/ProductDetailDTO';


@Injectable({
    providedIn: 'root'
})


export class ProductsService {


    constructor(private http: HttpClient) {

    }

    getFilteredProducts(filter: FilterProductDTO): Observable<IResponseResult<FilterProductDTO>> {
        //کد زیر برای سرچ کردن به کار میره یعنی میره اینارو میاره که با سرچ تاثیر بزاره
        let params;
        if (filter !== null) {
            params = new HttpParams()
                .set('pageId', filter.pageId.toString())
                .set('title', filter.title)
                .set('startPrice', filter.startPrice.toString())
                .set('endPrice', filter.endPrice.toString())
                .set('takeEntity', filter.takeEntity.toString());

            for (const category of filter.categories) {
                params = params.append('categories', category.toString());
            }

            if (filter.orderBy !== null) {
                params = params.append('orderBy', filter.orderBy.toString());
            }
        }
        return this.http.get<IResponseResult<FilterProductDTO>>('/products/filter-products', { params });
    }

    getproductActiveCategories(): Observable<IResponseResult<ProductCategory[]>> {
        return this.http.get<IResponseResult<ProductCategory[]>>('/products/product-active-categories');
    }

    getSingleProduct(productId: number): Observable<IResponseResult<ProductDetailDTO>> {
        return this.http.get<IResponseResult<ProductDetailDTO>>('/products/single-product/' + productId);
    }

    getProductComment(productId: number): Observable<IResponseResult<ProductCommentDTO[]>> {
        return this.http.get<IResponseResult<ProductCommentDTO[]>>('/products/product-comments/' + productId);
    }

    addProductComment(comment:AddProductComment):Observable<IResponseResult<ProductCommentDTO>>{
        return this.http.post<IResponseResult<ProductCommentDTO>>('/products/add-product-comment',comment);
    }
}