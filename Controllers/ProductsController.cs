using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessAnalytic.Models.Concrete.EfCore;
using BusinessAnalytic.Models.Entities;
using BusinessAnalytic.Models.Abstract;
using X.PagedList;
using BusinessAnalytic.Models.Concrete.EfCore.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace BusinessAnalytic.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;
        public ProductsController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }
        // GET: Products
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.ProductRepository.GetAll().Include(p => p.ProductModel).Include(p => p.ProductSubcategory).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _efCoreUnitOfWork.ProductRepository.Details(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["ProductModelId"] = new SelectList(_efCoreUnitOfWork.ProductModelsRepository.GetAll(), "ProductModelId", "Name");
            ViewData["ProductSubcategoryId"] = new SelectList(_efCoreUnitOfWork.ProductSubCategoriesRepository.GetAll(), "ProductSubcategoryId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode");
            ViewData["WeightUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                _efCoreUnitOfWork.ProductRepository.Create(product);
                await _efCoreUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductModelId"] = new SelectList(_efCoreUnitOfWork.ProductModelsRepository.GetAll(), "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_efCoreUnitOfWork.ProductSubCategoriesRepository.GetAll(), "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _efCoreUnitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductModelId"] = new SelectList(_efCoreUnitOfWork.ProductModelsRepository.GetAll(), "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_efCoreUnitOfWork.ProductSubCategoriesRepository.GetAll(), "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _efCoreUnitOfWork.ProductRepository.Update(product);
                    await _efCoreUnitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_efCoreUnitOfWork.ProductRepository.ProductExists(product.ProductId))
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
            ViewData["ProductModelId"] = new SelectList(_efCoreUnitOfWork.ProductModelsRepository.GetAll(), "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_efCoreUnitOfWork.ProductSubCategoriesRepository.GetAll(), "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_efCoreUnitOfWork.UnitMeasuresRepository.GetAll(), "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _efCoreUnitOfWork.ProductRepository.Details(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _efCoreUnitOfWork.ProductRepository.GetById(id);
            _efCoreUnitOfWork.ProductRepository.Delete(product);
            await _efCoreUnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
