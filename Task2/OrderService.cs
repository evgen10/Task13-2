using DB;
using DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Task2.Models;

namespace Task2
{
    public class OrderService
    {      
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        
        public IEnumerable<Order> GetOrders(RequestParams requestParams)
        {
            using (var db = new Context("Northwind"))
            {
                IQueryable<Order> orders = db.Orders;

                //var orders2 = db.Orders.GroupBy(o => o.CustomerID).Select(g => new { Name = g.Key, Count = g.Count() }).ToList();

                if (requestParams != null)
                { 
                    if (!string.IsNullOrEmpty(requestParams.Customer))
                    {
                        orders = db.Orders.Where(o => o.CustomerID == requestParams.Customer);
                    }

                    if (requestParams.DateFrom != null && requestParams.DateTo == null)
                    {
                        orders = orders.Where(o => o.OrderDate >= requestParams.DateFrom);

                    }
                    else if (requestParams.DateFrom == null && requestParams.DateTo != null)
                    {
                        orders = orders.Where(o => o.OrderDate <= requestParams.DateTo);
                    }
                    else if (requestParams.DateFrom != null && requestParams.DateTo != null)
                    {
                        orders = orders.Where(o => o.OrderDate <= requestParams.DateTo && o.OrderDate >= requestParams.DateFrom);
                    }

                    orders = orders.OrderBy(o => o.OrderDate);

                    if (requestParams.Skip != 0)
                    {
                        orders = orders.Skip(requestParams.Skip);
                    }

                    if (requestParams.Take != 0)
                    {
                        orders = orders.Take(requestParams.Take);
                    }                  
                }              

                return orders.ToList();
            }
        }
    }
}