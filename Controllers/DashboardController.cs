using BusinessAnalytic.Models.ChartModels;
using BusinessAnalytic.Models.Concrete.EfCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusinessAnalytic.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AdventureWorks2019Context _context;

        public DashboardController(AdventureWorks2019Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult VisualizePieChart()
        {
            List<ProductPieChart> productPieCharts = new List<ProductPieChart>();
            productPieCharts = _context.TransactionHistories.Include(t => t.Product).OrderBy(x => x.ActualCost).Take(20).Select(x => new ProductPieChart
            {
                Name = x.Product.Name,
                ActualCost = x.Quantity
            }).ToList();

            return Json(new { JSONList = productPieCharts });
        }
        public JsonResult VisualizeSellPieChart()
        {
            List<ProductSellPieChart> productSellPieCharts = new List<ProductSellPieChart>();
            productSellPieCharts = _context.SalesOrderHeaders.Include(s => s.BillToAddress).Include(s => s.CreditCard).Include(s => s.CurrencyRate).Include(s => s.Customer).Include(s => s.SalesPerson).Include(s => s.ShipMethod).Include(s => s.ShipToAddress).Include(s => s.Territory).Take(20).Select(x => new ProductSellPieChart
            {
                Name = x.Customer.Person.FirstName + " " + x.Customer.Person.LastName,
                ActualCost = x.SubTotal
            }).ToList();

            return Json(new { JSONList = productSellPieCharts });
        }
        public JsonResult VisualizeWarehouseColumnChart()
        {
            List<WarehouseColumnChart> warehouseColumnCharts = new List<WarehouseColumnChart>();
            warehouseColumnCharts = _context.ProductInventories.Include(p => p.Location).Include(p => p.Product).Take(20).Select(x => new WarehouseColumnChart
            {
                Name = x.Location.Name,
                Quantity = x.Quantity
            }).ToList();

            return Json(new { JSONList = warehouseColumnCharts });
        }
        public JsonResult VisualizeVendorColumnChart()
        {
            List<VendorColumnChart> profitColumnCharts = new List<VendorColumnChart>();
            profitColumnCharts = _context.ProductVendors.Include(p => p.BusinessEntity).Include(p => p.Product).Include(p => p.UnitMeasureCodeNavigation).Take(20).Select(x => new VendorColumnChart
            {
                Name = x.Product.Name,
                StandardPrice = x.StandardPrice
            }).ToList();

            return Json(new { JSONList = profitColumnCharts });
        }
    }
}
