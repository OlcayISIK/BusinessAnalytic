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
using Microsoft.AspNetCore.Authorization;
using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Concrete.EfCore.UnitOfWork;

namespace BusinessAnalytic.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;
        public CustomersController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: Customers
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.CustomersRepository.GetAll().Include(c => c.Person).Include(c => c.Store).Include(c => c.Territory).ToPagedList(page, 8);
            return View(adventureWorks2019Context);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _efCoreUnitOfWork.CustomersRepository.Details(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}
