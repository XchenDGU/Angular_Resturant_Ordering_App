import { Injectable } from '@angular/core';
import { CustomerOrder } from './customer-order.model';
import { OrderItem } from './order-item.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  formData:CustomerOrder;
  orderItems:OrderItem[];

  constructor(private http:HttpClient) { }

  getCurrentTime():string{
    var currTime = new Date();
    var timeInJSON = new Date(currTime.getTime()-(currTime.getTimezoneOffset() * 60000)).toJSON();
    return timeInJSON;  
  }

  saveOrUpdateOrder(){
    var body={
      ...this.formData,
      OrderDate:this.getCurrentTime(),
      OrderedItems:this.orderItems
    }
    console.log(body);
    return this.http.post(environment.apiURL+'/CustomerOrder',body);
  }

  getOrderList(){
    return this.http.get(environment.apiURL+'/CustomerOrder').toPromise();
  }

  getOrderByID(id:number):any{
    return this.http.get(environment.apiURL+'/CustomerOrder/'+id).toPromise();
  }

  deleteOrder(orderID:number){
    return this.http.delete(environment.apiURL+'/CustomerOrder/'+orderID).toPromise();
  }
}
