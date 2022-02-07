import { HttpClient, HttpParams } from "@angular/common/http";
import { BehaviorSubject, Observable } from "rxjs";
import { Injectable } from '@angular/core';
import { IResponseResult } from "../DTOs/Common/IResponseResult";
import { OrderBasketDetailDTO } from "../DTOs/Orders/OrderBasketDetailDTO";


@Injectable({
    providedIn: 'root'
})


export class OrderService {

    private orderDetails: BehaviorSubject<OrderBasketDetailDTO[]> = new BehaviorSubject<OrderBasketDetailDTO[]>(null);

    constructor(private http: HttpClient) { }

    _setOrderDetails(details: OrderBasketDetailDTO[]) {
        this.orderDetails.next(details);
    }

    _getOrderDetails(): Observable<OrderBasketDetailDTO[]> {
        return this.orderDetails;
    }

    addProdcuvtToOrder(productId: number, count: number): Observable<IResponseResult<{message:string,returnData:OrderBasketDetailDTO[]}>> {
        const params = new HttpParams().set('productId', productId.toString()).set('count', count.toString());
        return this.http.get<IResponseResult<{message:string,returnData:OrderBasketDetailDTO[]}>>('/order/add-order', { params });
    }

    getUserBasketDetail(): Observable<IResponseResult<OrderBasketDetailDTO[]>> {
        return this.http.get<IResponseResult<OrderBasketDetailDTO[]>>('/order/get-order-details');
    }

    removeOrderDetail(detailId:number):Observable<IResponseResult<OrderBasketDetailDTO[]>>{
        return this.http.get<IResponseResult<OrderBasketDetailDTO[]>>('/order/remove-order-details/'+detailId);
    }

} 