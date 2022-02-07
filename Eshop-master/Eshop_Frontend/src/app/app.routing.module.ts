import { NgModule } from '@angular/core';
import { Routes , RouterModule } from '@angular/router';
import { AboutUsComponent } from './pages/about-us/about-us.component';
import { EditAccountComponent } from './pages/account/edit-account/edit-account.component';
import { UserBasketComponent } from './pages/account/user-basket/user-basket.component';
import { ActivateAccountComponent } from './pages/activate-account/activate-account.component';
import { ContactUsComponent } from './pages/contact-us/contact-us.component';
import { IndexComponent } from './pages/index/index.component';
import { LoginComponent } from './pages/login/login.component';
import { ProductDetailComponent } from './pages/product-detail/product-detail.component';
import { ProductsComponent } from './pages/products/products.component';
import { RegisterComponent } from './pages/register/register.component';
import { UserAuthGuard } from './Utilities/UserAuthGuard';

const appRoutes: Routes = [
    { path: '', component: IndexComponent},
    { path: 'ContactUs', component: ContactUsComponent},
    { path: 'AboutUs', component: AboutUsComponent},
    { path: 'Login', component: LoginComponent},
    { path: 'Register', component: RegisterComponent},
    { path: 'activate-account/:activeCode', component: ActivateAccountComponent},
    { path: 'products', component: ProductsComponent},
    { path: 'products/:productId/:productName', component: ProductDetailComponent},
    //کد زیر یعنی اگر لاگین نبود ببند
    { path: 'user/edit', component: EditAccountComponent,canActivate:[UserAuthGuard]},
    { path: 'user/basket', component: UserBasketComponent,canActivate:[UserAuthGuard]}
];


@NgModule({
    imports:[RouterModule.forRoot(appRoutes)],
    exports:[RouterModule]
})

export class AppRoutingModule {

}