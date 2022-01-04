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
    public class WarehousesController : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public WarehousesController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: Warehouses
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.WarehousesRepository.GetAll().Include(p => p.Location).Include(p => p.Product).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }     
        // GET: Warehouses/Create
        public IActionResult Create()
        {
            ViewData["LocationId"] = new SelectList(_efCoreUnitOfWork.LocationsRepository.GetAll(), "LocationId", "Name");
            ViewData["ProductId"] = new SelectList(_efCoreUnitOfWork.ProductRepository.GetAll(), "ProductId", "Name");
            return View();
        }

        // POST: Warehouses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,LocationId,Shelf,Bin,Quantity,Rowguid,ModifiedDate")] ProductInventory productInventory)
        {
            if (ModelState.IsValid)
            {
                _efCoreUnitOfWork.WarehousesRepository.Create(productInventory);
                await _efCoreUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationId"] = new SelectList(_efCoreUnitOfWork.LocationsRepository.GetAll(), "LocationId", "Name", productInventory.LocationId);
            ViewData["ProductId"] = new SelectList(_efCoreUnitOfWork.ProductRepository.GetAll(), "ProductId", "Name", productInventory.ProductId);
            return View(productInventory);
        }
    }
}
