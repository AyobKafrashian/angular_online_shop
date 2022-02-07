import { Component, OnInit , ViewChild} from '@angular/core';
import { Product } from 'src/app/DTOs/Products/Product';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from 'src/app/services/products.service';
import { ImagePath, ImageGalleryPath } from 'src/app/Utilities/PathTools';
import { ProductGallery } from 'src/app/DTOs/Products/ProductGallery';
import { ProductCommentDTO } from 'src/app/DTOs/Products/ProductCommentDTO';
import { CurrentUser } from 'src/app/DTOs/Account/CurrentUser';
import { AuthService } from 'src/app/services/auth.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AddProductComment } from 'src/app/DTOs/Products/AddProductComment';
import { OrderService } from 'src/app/services/Order.Service';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit {


  @ViewChild('sweetAlert') private sweetAlert: SwalComponent;
  @ViewChild('sweetAlertError') private sweetAlertError: SwalComponent;

  public imagePath: string = ImagePath;
  public imageGalleryPath: string = ImageGalleryPath;
  product: Product;
  galleries: ProductGallery[];
  mainImage: string;
  selectedImageId = 0;
  relatedProduct: Product;
  productComments: ProductCommentDTO[];
  currentUser: CurrentUser = null;
  commentForm: FormGroup;
  count = 1;


  constructor(private productService: ProductsService, private activatedRouth: ActivatedRoute, private routher: Router, private authService: AuthService, private orderService: OrderService) { }

  ngOnInit(): void {

    this.authService.getCurrentUser().subscribe(res => {
      if (res !== null) {
        this.currentUser = res;
      }
    });

    this.activatedRouth.params.subscribe(params => {

      const productId = params.productId;

      if (productId === undefined) {
        this.routher.navigate(['']);
      }

      //#region Get Single Product
      this.productService.getSingleProduct(productId).subscribe(res => {
        if (res.status === 'NotFound') {
          this.routher.navigate(['']);
        }
        else if (res.status === 'Success') {
          this.product = res.data.product;
          this.galleries = res.data.galleries;
          this.mainImage = this.imagePath + this.product.imageName;
          this.relatedProduct = res.data.relatedProduct;
        }
      });
      //#endregion

      //#region Get Product Commnet
      this.productService.getProductComment(productId).subscribe(res => {
        this.productComments = res.data;
      });
      //#endregion

    });

    this.commentForm = new FormGroup({
      text: new FormControl(null, [
        Validators.required,
        Validators.maxLength(100)
      ])

    });

  }

  selectImage(imageId: number) {
    this.selectedImageId = imageId;
    if (imageId !== 0) {
      const gallery = this.galleries.filter(g => g.id === imageId)[0];
      this.mainImage = this.imageGalleryPath + gallery.imageName;
    }
    else {
      this.mainImage = this.imagePath + this.product.imageName;
    }
  }

  addComment() {
    if (this.commentForm.valid) {
      const comment = new AddProductComment(this.product.id, this.commentForm.controls.text.value);

      //add comment to database
      this.productService.addProductComment(comment).subscribe(res => {
        if (res.status === 'Success') {
          const commentDTO = res.data;
          commentDTO.userFullName = this.currentUser.firstName + ' ' + this.currentUser.lastName;
          //کد زیر آنشیفت یعنی بعد از ساکسس بودن کامنت را به اول میبرد
          this.productComments.unshift(commentDTO);
          this.commentForm.reset();
        }
      });
    }
  }

  addCount() {
    this.count += 1;
  }

  removeCount() {
    if (this.count > 1) {
      this.count -= 1;
    }  
  }

  addProductToOrder() {
    const productId = this.product.id;
    const count = this.count;

    if (count >= 1) {
      this.orderService.addProdcuvtToOrder(productId, count).subscribe(res => {
        if(res.status==='Success'){
          this.sweetAlert.text = res.data['message'];
          this.sweetAlert.fire();
          this.orderService._setOrderDetails(res.data.returnData)
        }
        if(res.status === 'Error'){
          this.sweetAlertError.text = res.data['message'];
          this.sweetAlertError.fire(); 
        }
      });
    }
  }

}
