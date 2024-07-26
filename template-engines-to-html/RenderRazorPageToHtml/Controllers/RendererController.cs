using Microsoft.AspNetCore.Mvc;
using RenderRazorPageToHtml.Helpers;
using RenderRazorPageToHtml.Models;
using System.Reflection;

namespace RenderRazorPageToHtml.Controllers
{
    public class RendererController : Controller
    {
        private readonly RazorViewToStringRenderer _razorViewToStringRenderer;
        public RendererController(RazorViewToStringRenderer razorViewToStringRenderer)
        {
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }
        public async Task<IActionResult> CustomerAsync()
        {
            //Model
            var customer = new Customer
            {
                Id = 1,
                Name = "Jimmy Roe",
                Address = "123 Nowhere Lane\r\nAnytown, USA",
                Email = "jroe@doeboy.com"
            };

            //Create an action context and specify the correct path to your view
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);
            string viewString = await _razorViewToStringRenderer.RenderViewToStringAsync(actionContext, "Views/Samples/Invoice.cshtml", customer);

            // Do something with the rendered view string
            return Content(viewString);
        }
        public async Task<IActionResult> InvoiceAsync()
        {
            //Model
            var invoice = new Invoice
            {
                InvoiceNumber = "INV-1001",
                InvoiceDate = DateTime.Now,
                CustomerName = "John Doe",
                CustomerAddress = "123 Main Street",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Description = "Item 1", Quantity = 2, UnitPrice = 10.00m },
                    new InvoiceItem { Description = "Item 2", Quantity = 1, UnitPrice = 20.00m },
                    new InvoiceItem { Description = "Item 3", Quantity = 5, UnitPrice = 5.00m },
                }
            };

            //Create an action context and specify the correct path to your view
            var actionContext = new ActionContext(HttpContext, RouteData, ControllerContext.ActionDescriptor);
            string viewString = await _razorViewToStringRenderer.RenderViewToStringAsync(actionContext, "Views/Samples/Invoice.cshtml", invoice);

            // Do something with the rendered view string
            return Content(viewString);
        }
    }
}
