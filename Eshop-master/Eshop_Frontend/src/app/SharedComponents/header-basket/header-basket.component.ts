import { Component, OnInit } from '@angular/core';
import { OrderBasketDetailDTO } from 'src/app/DTOs/Orders/OrderBasketDetailDTO';
import { OrderService } from 'src/app/services/Order.Service';

@Component({
  selector: 'app-header-basket',
  templateUrl: './header-basket.component.html',
  styleUrls: ['./header-basket.component.scss']
})
export class HeaderBasketComponent implements OnInit {

  details: OrderBasketDetailDTO[] = [];
  totalPrice = 0;

  constructor(public orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService._getOrderDetails().subscribe(res => {
      this.details = res;
      this.totalPrice = 0;
      if (this.details !== null) {
        for (let i = 0; i < this.details.length; i++) {
          this.totalPrice += this.details[i].price * this.details[i].count;
        }
      }
    });
  }

  removeOrderDetail(detailId: number) {
    this.orderService.removeOrderDetail(detailId).subscribe(res => {
      if (res.status === 'Success') {
        this.details = res.data;
        this.orderService._setOrderDetails(res.data);
      }
    });
  }
}