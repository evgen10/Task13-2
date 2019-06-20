using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task2;
using Task2.Models;

namespace HandlerTest
{
    [TestClass]
    public class OrderServiceTest
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private OrderService orderService = new OrderService();




        [TestMethod]
        public void TestMethod1()
        {
            RequestParams requestParams = new RequestParams()
            {
                Customer = "SAVEA",
                DateFrom = DateTime.Parse("8/11/1997"),
                DateTo = DateTime.Parse("3/30/1998"),
                Skip = 0,
                Take = 0
            };

            RequestParams requestParams2 = new RequestParams()
            {
                Customer = "SAVEA",
                DateFrom = DateTime.Parse("8/11/1997"),
                DateTo = DateTime.Parse("3/30/1998"),
                Skip = 0,
                Take = 0
            };

            RequestParams requestParams3 = new RequestParams()
            {
                Customer = "SAVEA",
                DateFrom = DateTime.Parse("8/11/1997"),
                DateTo = DateTime.Parse("3/30/1998"),
                Skip = 0,
                Take = 0
            };

            RequestParams requestParams4 = new RequestParams()
            {
                Customer = null,
                DateFrom = null,
                DateTo = DateTime.Parse("8/11/1997"),
                Skip = 0,
                Take = 0
            };

            var dd = orderService.GetOrders(requestParams4);
        }
    }
}
