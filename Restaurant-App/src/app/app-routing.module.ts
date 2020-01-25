import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CustomerOrdersComponent } from './customer-orders/customer-orders.component';
import { OrderComponent } from './customer-orders/order/order.component';


const routes: Routes = [
  {path:'',redirectTo:'order',pathMatch:'full'},
  {path:'orders',component:CustomerOrdersComponent},
  {path:'order',children:[
    {path:'',component:OrderComponent},
    {path:'edit/:id',component:OrderComponent}
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
