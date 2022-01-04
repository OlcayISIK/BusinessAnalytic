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
    public class SalesTerritoriesController : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public SalesTerritoriesController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: SalesTerritories
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.SalesTerritoriesRepository.GetAll().Include(s => s.CountryRegionCodeNavigation).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: SalesTerritories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesTerritory = await _efCoreUnitOfWork.SalesTerritoriesRepository.Details(id);
            if (salesTerritory == null)
            {
                return NotFound();
            }

            return View(salesTerritory);
        }
    }
}
