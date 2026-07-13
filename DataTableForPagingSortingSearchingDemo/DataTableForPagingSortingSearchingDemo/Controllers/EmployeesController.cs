using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataTableForPagingSortingSearchingDemo.Models;

namespace DataTableForPagingSortingSearchingDemo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        //public async Task<IActionResult> Index()
        //{
        //    var appDbContext = _context.Employees.Include(e => e.Countries).Include(e => e.Departments);
        //    return View(await appDbContext.ToListAsync());
        //}

        public IActionResult Index()
        {           
            return View();
        }

        public IActionResult LoadData()
        {
            IQueryable<Employee> EmployeeList = _context.Employees.Include(e => e.Countries).Include(e => e.Departments);

            #region Sorting

            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            //Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                switch (sortColumn)
                {
                    case "firstName":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.FirstName);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.FirstName);
                                break;

                        }

                        break;
                    case "lastName":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.LastName);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.LastName);
                                break;

                        }
                        break;
                    case "title":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.Title);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.Title);
                                break;

                        }
                        break;
                    case "birthDate":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.BirthDate);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.BirthDate);
                                break;

                        }
                        break;
                    case "hireDate":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.HireDate);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.HireDate);
                                break;

                        }
                        break;
                    case "countries.countryName":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.Countries.CountryName);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.Countries.CountryName);
                                break;

                        }
                        break;
                    case "departments.deptName":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.Departments.DeptName);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.Departments.DeptName);
                                break;

                        }
                        break;
                    case "notes":
                        switch (sortColumnDirection)
                        {
                            case "asc":
                                EmployeeList = EmployeeList.OrderBy(c => c.Notes);
                                break;
                            case "desc":
                                EmployeeList = EmployeeList.OrderByDescending(c => c.Notes);
                                break;

                        }
                        break;
                }
            }
            #endregion


            #region Search Functionality           

            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            if (!string.IsNullOrEmpty(searchValue))
            {
                EmployeeList = EmployeeList.Where(e => e.FirstName.Contains(searchValue) || e.LastName.Contains(searchValue)
                || e.Title.Contains(searchValue) || e.Countries.CountryName.Contains(searchValue) ||
                e.Departments.DeptName.Contains(searchValue));
            }
            #endregion


            #region Paging Functionality
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;



            var actualdata = EmployeeList.ToList();
            int recordsTotal = 0;
            recordsTotal = actualdata.Count();

            actualdata = EmployeeList.Skip(skip).Take(pageSize).ToList();

            #endregion

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = actualdata });
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Countries)
                .Include(e => e.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryID", "CountryName");
            ViewData["DeptID"] = new SelectList(_context.Departments, "DeptID", "DeptName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Title,BirthDate,HireDate,CountryID,DeptID,Notes")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryID", "CountryName", employee.CountryID);
            ViewData["DeptID"] = new SelectList(_context.Departments, "DeptID", "DeptName", employee.DeptID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryID", "CountryName", employee.CountryID);
            ViewData["DeptID"] = new SelectList(_context.Departments, "DeptID", "DeptName", employee.DeptID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Title,BirthDate,HireDate,CountryID,DeptID,Notes")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["CountryID"] = new SelectList(_context.Countries, "CountryID", "CountryName", employee.CountryID);
            ViewData["DeptID"] = new SelectList(_context.Departments, "DeptID", "DeptName", employee.DeptID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Countries)
                .Include(e => e.Departments)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
