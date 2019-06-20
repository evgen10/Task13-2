using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB;
using Task2.Models;
using DB.Model;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace Task2
{
    public class ReportHandler : IHttpHandler
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private string providerName = "SqlServer";
        private OrderService orderService = new OrderService();

      
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            var requestParams = context.Request.QueryString;
            var param = context.Request.QueryString["param1"];

            //try
            //{
            //    var prms = CreateParams(requestParams);
            //    var orders = orderService.GetOrders(prms);
            //}
            //catch (Exception e)
            //{
            //    throw;
            //}     

            //context.Response.Output.WriteLine("HelloWorld");    

            

                context.Response.BinaryWrite(GetDoc());
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
               context.Response.End();
            }

        
        

        private byte[] GetDoc()
        {
            MemoryStream stream1 = new MemoryStream();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(stream1, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());


                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "TestSheet" };

                sheets.Append(sheet);                

                workbookPart.Workbook.Save();

                return stream1.ToArray();
            }
        }

        private RequestParams CreateParams(NameValueCollection queryString)
        { 
            RequestParams requestParams = new RequestParams();

            if (queryString["customer"] != null)
            {
                requestParams.Customer = queryString["customer"];
            }     

            if (queryString["dateFrom"]!= null)
            {
                requestParams.DateFrom = DateTime.Parse(queryString["dateFrom"]);
            }

            if (queryString["dateTo"] != null)
            {
                requestParams.DateTo = DateTime.Parse(queryString["dateTo"]);
            }

            if (queryString["take"] != null)
            {
                requestParams.Take = int.Parse(queryString["take"]);
            }

            if (queryString["skip"] != null)
            {
                requestParams.Skip = int.Parse(queryString["skip"]);
            }

            return requestParams;
        }       
    }
}
