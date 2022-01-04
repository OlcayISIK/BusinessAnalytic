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
    public class ProfitController : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public ProfitController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: Profit
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.ProfitRepository.GetAll().Include(p => p.BusinessEntity).Include(p => p.Product).Include(p => p.UnitMeasureCodeNavigation).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: Profit/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVendor = await _efCoreUnitOfWork.ProfitRepository.Details(id);
            if (productVendor == null)
            {
                return NotFound();
            }

            return View(productVendor);
        }

        // GET: Profit/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.VendorsRepository.GetAll(), "BusinessEntityId", "AccountNumber");
            ViewData["ProductId"] = new SelectList(_efCoreUnitOfWork.ProductRepository.GetAll(), "ProductId", "Name");
            ViewData["UnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: Profit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,BusinessEntityId,AverageLeadTime,StandardPrice,LastReceiptCost,LastReceiptDate,MinOrderQty,MaxOrderQty,OnOrderQty,UnitMeasureCode,ModifiedDate")] ProductVendor productVendor)
        {
            if (ModelState.IsValid)
            {
                _efCoreUnitOfWork.ProfitRepository.Create(productVendor);
                await _efCoreUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.VendorsRepository.GetAll(), "BusinessEntityId", "AccountNumber", productVendor.BusinessEntityId);
            ViewData["ProductId"] = new SelectList(_efCoreUnitOfWork.ProductRepository.GetAll(), "ProductId", "Name", productVendor.ProductId);
            ViewData["UnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", productVendor.UnitMeasureCode);
            return View(productVendor);
        }

        // GET: Profit/Edit/5
        public async Task<IActionResult> Edit(int? id, int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVendor = await _efCoreUnitOfWork.ProfitRepository.GetByTwoId(id, id2);
            if (productVendor == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.VendorsRepository.GetAll(), "BusinessEntityId", "AccountNumber", productVendor.BusinessEntityId);
            ViewData["ProductId"] = new SelectList(_efCoreUnitOfWork.ProductRepository.GetAll(), "ProductId", "Name", productVendor.ProductId);
            ViewData["UnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", productVendor.UnitMeasureCode);
            return View(productVendor);
        }

        // POST: Profit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,BusinessEntityId,AverageLeadTime,StandardPrice,LastReceiptCost,LastReceiptDate,MinOrderQty,MaxOrderQty,OnOrderQty,UnitMeasureCode,ModifiedDate")] ProductVendor productVendor)
        {
            if (id != productVendor.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _efCoreUnitOfWork.ProfitRepository.Update(productVendor);
                    await _efCoreUnitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_efCoreUnitOfWork.ProfitRepository.ProductVendorExists(productVendor.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.VendorsRepository.GetAll(), "BusinessEntityId", "AccountNumber", productVendor.BusinessEntityId);
            ViewData["ProductId"] = new SelectList(_efCoreUnitOfWork.ProductRepository.GetAll(), "ProductId", "Name", productVendor.ProductId);
            ViewData["UnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", productVendor.UnitMeasureCode);
            return View(productVendor);
        }

        // GET: Profit/Delete/5
        public async Task<IActionResult> Delete(int? id,int? id2)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productVendor = await _efCoreUnitOfWork.ProfitRepository.GetByTwoId(id, id2);
            if (productVendor == null)
            {
                return NotFound();
            }

            return View(productVendor);
        }

        // POST: Profit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,int id2)
        {
            var productVendor = await _efCoreUnitOfWork.ProfitRepository.GetByTwoId(id, id2);
            _efCoreUnitOfWork.ProfitRepository.Delete(productVendor);
            await _efCoreUnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
