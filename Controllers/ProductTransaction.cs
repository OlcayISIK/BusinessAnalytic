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
    public class ProductTransaction : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public ProductTransaction(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: ProductTransaction
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.ProductTransactionRepository.GetAll().Include(t => t.Product).OrderBy(x => x.Quantity).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: ProductTransaction/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionHistory = await _efCoreUnitOfWork.ProductTransactionRepository.Details(id);   
            if (transactionHistory == null)
            {
                return NotFound();
            }

            return View(transactionHistory);
        }
    }
}
