import { Component, OnInit } from '@angular/core';
import { OrderBasketDetailDTO } from 'src/app/DTOs/Orders/OrderBasketDetailDTO';
import { OrderService } from 'src/app/services/Order.Service';

@Component({
  selector: 'app-user-basket',
  templateUrl: './user-basket.component.html',
  styleUrls: ['./user-basket.component.scss']
})
export class UserBasketComponent implements OnInit {

  details: OrderBasketDetailDTO[] = [];
  totalPrice = 0;

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
    this.orderService._getOrderDetails().subscribe(res => {
      this.details = res;
      this.totalPrice = 0;
      if (this.totalPrice !== null) {
        for (let i = 0; i < this.details.length; i++) {
          this.totalPrice += this.details[i].price * this.details[i].count;
        }
      }
    });
  }

  removeOrderDetail(detaildId: number) {
    this.orderService.removeOrderDetail(detaildId).subscribe(res => {
      if (res.status === 'Success') {
        this.orderService._setOrderDetails(res.data);
      }
    });
  }

}
