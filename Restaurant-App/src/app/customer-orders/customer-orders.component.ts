import { Component, OnInit } from '@angular/core';
import { OrderService } from '../shared/order.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-orders',
  templateUrl: './customer-orders.component.html',
  styles: []
})
export class CustomerOrdersComponent implements OnInit {
  orderList;
  constructor(private orderService:OrderService,
    private router:Router,
    private toastr:ToastrService) { }

  ngOnInit() {
    this.refreshList();
  }
  openForEdit(orderID:number){
    this.router.navigate(['/order/edit/'+orderID]);
  }

  refreshList(){
    this.orderService.getOrderList().then(res=>this.orderList=res);
  }

  onOrderDelete(orderID:number){
    if(confirm("Are you sure to delete this order")){
      this.orderService.deleteOrder(orderID).then(res=>{
        this.refreshList();
        this.toastr.warning("Deleted Successfully","Restaurant App")
      })
    }
  }
}
