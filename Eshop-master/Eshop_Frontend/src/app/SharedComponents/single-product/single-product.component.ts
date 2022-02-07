import { Component, OnInit, Input,ViewChild } from '@angular/core';
import { Product } from 'src/app/DTOs/Products/Product';
import { OrderService } from 'src/app/services/Order.Service';
import { ImagePath } from 'src/app/Utilities/PathTools';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';

@Component({
  selector: 'app-single-product',
  templateUrl: './single-product.component.html',
  styleUrls: ['./single-product.component.scss']
})
export class SingleProductComponent implements OnInit {

  @ViewChild('sweetAlert') private sweetAlert: SwalComponent;
  @ViewChild('sweetAlertError') private sweetAlertError: SwalComponent;

  @Input() product: Product;
  public imagePath: string = ImagePath;
  public productName: string;

  constructor(private orderService:OrderService) { }

  ngOnInit(): void {
    this.productName = this.product.productName.replace(/\s/g, '-');
  }


  addProductToOrder() {
    const productId = this.product.id;
    const count = 1;

    this.orderService.addProdcuvtToOrder(productId, count).subscribe(res => {
      if (res.status === 'Success') {
        this.sweetAlert.text = res.data['message'];
        this.sweetAlert.fire();
        this.orderService._setOrderDetails(res.data.returnData);
      }
      if (res.status === 'Error') {
        this.sweetAlertError.text = res.data['message'];
        this.sweetAlertError.fire();
      }
    });
  }

}
