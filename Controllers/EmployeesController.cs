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
    public class EmployeesController : Controller
    {

        private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;

        public EmployeesController(IEfCoreUnitOfWork efCoreUnitOfWork)
        {
            _efCoreUnitOfWork = efCoreUnitOfWork;
        }

        // GET: Employees
        public IActionResult Index(int page = 1)
        {
            var adventureWorks2019Context = _efCoreUnitOfWork.EmployeeRepository.GetAll().Include(e => e.BusinessEntity).ToPagedList(page,8);
            return View(adventureWorks2019Context);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _efCoreUnitOfWork.EmployeeRepository.Details(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.PeopleRepository.GetAll(), "BusinessEntityId", "FirstName");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,NationalIdnumber,LoginId,OrganizationLevel,JobTitle,BirthDate,MaritalStatus,Gender,HireDate,SalariedFlag,VacationHours,SickLeaveHours,CurrentFlag,Rowguid,ModifiedDate")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _efCoreUnitOfWork.EmployeeRepository.Create(employee);
                await _efCoreUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.PeopleRepository.GetAll(), "BusinessEntityId", "FirstName", employee.BusinessEntityId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _efCoreUnitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.PeopleRepository.GetAll(), "BusinessEntityId", "FirstName", employee.BusinessEntityId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,NationalIdnumber,LoginId,OrganizationLevel,JobTitle,BirthDate,MaritalStatus,Gender,HireDate,SalariedFlag,VacationHours,SickLeaveHours,CurrentFlag,Rowguid,ModifiedDate")] Employee employee)
        {
            if (id != employee.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _efCoreUnitOfWork.EmployeeRepository.Update(employee);
                    await _efCoreUnitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_efCoreUnitOfWork.EmployeeRepository.EmployeeExists(employee.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_efCoreUnitOfWork.PeopleRepository.GetAll(), "BusinessEntityId", "FirstName", employee.BusinessEntityId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _efCoreUnitOfWork.EmployeeRepository.Details(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _efCoreUnitOfWork.EmployeeRepository.GetById(id);
            _efCoreUnitOfWork.EmployeeRepository.Delete(employee);
            await _efCoreUnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

    }
}
