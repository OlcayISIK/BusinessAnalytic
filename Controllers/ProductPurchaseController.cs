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
    public class ProductPurchaseController : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public ProductPurchaseController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: ProductPurchase
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.ProductPurchaseRepository.GetAll().Include(p => p.Employee).Include(p => p.ShipMethod).Include(p => p.Vendor).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: ProductPurchase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderHeader = await _efCoreUnitOfWork.ProductPurchaseRepository.Details(id);
            if (purchaseOrderHeader == null)
            {
                return NotFound();
            }

            return View(purchaseOrderHeader);
        }
    }
}
