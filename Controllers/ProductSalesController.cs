using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessAnalytic.Models.Concrete.EfCore;
using BusinessAnalytic.Models.Entities;
using X.PagedList;
using BusinessAnalytic.Models.Concrete.EfCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace BusinessAnalytic.Controllers
{
    [Authorize]
    public class ProductSalesController : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public ProductSalesController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: ProductSales
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.ProductSalesRepository.GetAll().Include(s => s.BillToAddress).Include(s => s.CreditCard).Include(s => s.CurrencyRate).Include(s => s.Customer).Include(s => s.SalesPerson).Include(s => s.ShipMethod).Include(s => s.ShipToAddress).Include(s => s.Territory).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: ProductSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrderHeader = await _efCoreUnitOfWork.ProductSalesRepository.Details(id);
            if (salesOrderHeader == null)
            {
                return NotFound();
            }

            return View(salesOrderHeader);
        }
    }
}
