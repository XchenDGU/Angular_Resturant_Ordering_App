using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Tutorial2_Food_Ordering_App.Models;

namespace Tutorial2_Food_Ordering_App.Controllers
{
    public class CustomerOrderController : ApiController
    {
        private RestaurantDBModel db = new RestaurantDBModel();

        // GET: api/CustomerOrder
        public System.Object GetCustomerOrders()
        {
            var result = (from a in db.CustomerOrders
                          join c in db.Customers
                          on a.CustomerID equals c.CustomerID
                          select new
                          {
                              a.OrderID,
                              a.OrderNo,
                              Customer = c.Name,
                              a.GTotal,
                              a.PMethod,
                          }).ToList();
            return result;
        }

        // GET: api/CustomerOrder/5
        [ResponseType(typeof(CustomerOrder))]
        public IHttpActionResult GetCustomerOrder(long id)
        {
            
            var order = (from a in db.CustomerOrders
                          where a.OrderID == id
                          select new
                          {
                              a.OrderID,
                              a.OrderNo,
                              a.CustomerID,
                              a.GTotal,
                              a.PMethod,
                              DeletedOrderItemIDs=""
                          }).FirstOrDefault();

            var orderDetails = (from a in db.OrderedItems
                                join b in db.Items
                                on a.ItemID equals b.ItemID
                                where a.OrderID == id
                                select new
                                {
                                    a.OrderID,
                                    a.OrderItemID,
                                    a.ItemID,
                                    a.Quantity,
                                    ItemName = b.Name,
                                    b.Price,
                                    Total = a.Quantity * b.Price
                                }).ToList();
            return Ok(new { order,orderDetails});
        }

        /*
        // PUT: api/CustomerOrder/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerOrder(long id, CustomerOrder customerOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerOrder.OrderID)
            {
                return BadRequest();
            }

            db.Entry(customerOrder).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        */
        // POST: api/CustomerOrder
        [ResponseType(typeof(CustomerOrder))]
        public IHttpActionResult PostCustomerOrder(CustomerOrder customerOrder)
        {
            //Have done validation in front-end
            
            try
            {
                
                //Insert or Update the Order table
                if(customerOrder.OrderID == 0)
                    db.CustomerOrders.Add(customerOrder);
                foreach (var item in customerOrder.OrderedItems)
                {
                    if (item.OrderItemID == 0)
                    {
                        item.OrderID = customerOrder.OrderID;
                        db.OrderedItems.Add(item);
                    }
                    else
                        db.Entry(item).State = EntityState.Modified; //save the changes
                }
                if(customerOrder.OrderID != 0)
                    db.Entry(customerOrder).State = EntityState.Modified;
                db.SaveChanges();
                //Insert or Update the orderedItems
                
                //delete for orderedItem
                foreach (var id in customerOrder.DeletedOrderItemIDs.Split(',').Where(x=>x!=""))
                {
                    OrderedItem x = db.OrderedItems.Find(Convert.ToInt64(id));
                    db.OrderedItems.Remove(x);
                }

                db.SaveChanges();

                //return CreatedAtRoute("DefaultApi", new { id = customerOrder.OrderID }, customerOrder);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // DELETE: api/CustomerOrder/5
        [ResponseType(typeof(CustomerOrder))]
        public IHttpActionResult DeleteCustomerOrder(long id)
        {
            CustomerOrder customerOrder = db.CustomerOrders.Include(y=>y.OrderedItems)
                .SingleOrDefault(x=>x.OrderID == id);
            foreach (var item in customerOrder.OrderedItems.ToList())
            {
                db.OrderedItems.Remove(item);
            }
            

            db.CustomerOrders.Remove(customerOrder);
            db.SaveChanges();

            return Ok(customerOrder);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerOrderExists(long id)
        {
            return db.CustomerOrders.Count(e => e.OrderID == id) > 0;
        }
    }
}