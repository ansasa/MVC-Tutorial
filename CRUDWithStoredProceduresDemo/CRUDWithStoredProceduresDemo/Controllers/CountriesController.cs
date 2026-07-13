using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDWithStoredProceduresDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDWithStoredProceduresDemo.Controllers
{
    public class CountriesController : Controller
    {
        private readonly AppDbContext _context;

        public CountriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Countries.ToListAsync());

            //var allCountries = await _context.Countries.FromSqlRaw("EXEC usp_GetAllCountries").ToListAsync();
            //var allCountries = await _context.Countries.FromSqlInterpolated($"EXEC usp_GetAllCountries").ToListAsync();


            var allCountries = await _context.Countries.FromSqlInterpolated($"SELECT * from Country Order By CountryName").ToListAsync();
            return View(allCountries);
        }

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var country = await _context.Countries
            //    .FirstOrDefaultAsync(m => m.CountryID == id);

            //var country = await _context.Countries.FromSqlRaw("EXEC usp_GetCountryDetailsById {0}",id).ToListAsync();
            //var country = await _context.Countries.FromSqlInterpolated($"EXEC usp_GetCountryDetailsById {id}").ToListAsync();

            var country = await _context.Countries.FromSqlInterpolated($"SELECT * from country where countryid={id}").ToListAsync();

            if (country == null)
            {
                return NotFound();
            }

            return View(country.FirstOrDefault() );
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CountryID,CountryName")] Country country)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(country);
                //await _context.SaveChangesAsync();


                //int Counter = await _context.Database.ExecuteSqlRawAsync("usp_Country_Insert {0}",country.CountryName );
                //int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"usp_Country_Insert @CountryName={country.CountryName}");

                var paramCountryID = new SqlParameter();
                paramCountryID.ParameterName = "@CountryID";
                paramCountryID.SqlDbType = SqlDbType.Int;
                paramCountryID.Direction = ParameterDirection.Output;

                //int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"usp_Country_Insert @CountryName={country.CountryName}, @CountryID={paramCountryID} out");
                ViewBag.CountryID = paramCountryID.Value;

                int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"insert into country(countryname) values({country.CountryName})");


                //return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var country = await _context.Countries.FindAsync(id);

            //var country = await _context.Countries.FromSqlInterpolated($"EXEC usp_GetCountryDetailsById {id}").ToListAsync();

            var country = await _context.Countries.FromSqlInterpolated($"SELECT * from country where countryid={id}").ToListAsync();
            if (country == null)
            {
                return NotFound();
            }
            return View(country.FirstOrDefault());
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,CountryName")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(country);
                    //await _context.SaveChangesAsync();

                    //int Counter = await _context.Database.ExecuteSqlRawAsync("usp_Country_Insert {0}",country.CountryName );
                    //int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"usp_Country_Update {country.CountryID},{country.CountryName}");

                    int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"update country set countryname={country.CountryName} where CountryID={id}");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryID))
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
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var country = await _context.Countries
            //    .FirstOrDefaultAsync(m => m.CountryID == id);

            //var country = await _context.Countries.FromSqlInterpolated($"EXEC usp_GetCountryDetailsById {id}").ToListAsync();

            var country = await _context.Countries.FromSqlInterpolated($"SELECT * from country where countryid={id}").ToListAsync();

            if (country == null)
            {
                return NotFound();
            }

            return View(country.FirstOrDefault());
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var country = await _context.Countries.FindAsync(id);
            // _context.Countries.Remove(country);
            //await _context.SaveChangesAsync();

            //int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"usp_Country_Delete {id}");

            int Counter = await _context.Database.ExecuteSqlInterpolatedAsync($"delete from country where countryid= {id}");

            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }
    }
}
